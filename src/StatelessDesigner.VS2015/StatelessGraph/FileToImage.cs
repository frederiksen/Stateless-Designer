using System;
using System.Drawing;
using System.IO;
using Microsoft.Glee;
using Microsoft.Glee.Splines;
using StatelessXml;
using P = Microsoft.Glee.Splines.Point;

namespace StatelessGraph
{
  public class FileToImage
  {
    private const int margin = 30;
    private static readonly Pen nodePen = new Pen(Color.DarkGray);
    private static readonly Pen edgePen = new Pen(Color.DarkGray);
    private static readonly Brush startNodeTextBrush = new SolidBrush(Color.FromArgb(0, 0, 139));
    private static readonly Brush nodeTextBrush = new SolidBrush(Color.FromArgb(0, 0, 139));
    private static readonly Brush edgeTextBrush = new SolidBrush(Color.FromArgb(0, 139, 139));
    private static readonly Font startNodeFont = new Font("Consolas", 10, FontStyle.Underline);
    private static readonly Font nodeFont = new Font("Consolas", 10);
    private static readonly Font edgeFont = new Font("Consolas", 8);
    private static readonly StringFormat stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
    private const double nodeWidth = 50;
    private const double nodeHeight = 20;
    private static string startNode = string.Empty;

    public static Bitmap ConvertFileToBitmap(string fileName)
    {
      try
      {
        var graph = CreateAndLayoutGraph(fileName);
        var bitmap = new Bitmap((int)graph.BoundingBox.Width, (int)graph.BoundingBox.Height);
        DrawGraph(bitmap, graph);
        return bitmap;
      }
      catch (Exception)
      {
        var thisAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        var file = thisAssembly.GetManifestResourceStream("StatelessGraph.Resources.Error.png");
        return new Bitmap(Image.FromStream(file));          
      }
    }

    private static void DrawGraph(Bitmap bitmap, GleeGraph graph)
    {
      var graphics = Graphics.FromImage(bitmap);
      graphics.Clear(Color.White);

      var matrix = new System.Drawing.Drawing2D.Matrix();
      matrix.Translate(- (float)graph.BoundingBox.Left, - (float)graph.BoundingBox.Bottom);
      graphics.Transform = matrix;

      // Nodes
      foreach (var node in graph.NodeMap.Values)
      {
        ICurve curve = node.MovedBoundaryCurve;
        Ellipse el = curve as Ellipse;
        if (el != null)
        {
          graphics.DrawEllipse(nodePen, new RectangleF((float) el.BBox.Left, (float) el.BBox.Bottom,
                                                       (float) el.BBox.Width, (float) el.BBox.Height));
          var font = (node.Id == startNode) ? startNodeFont : nodeFont;
          var textBrush = (node.Id == startNode) ? startNodeTextBrush : nodeTextBrush;
          graphics.DrawString(
            node.Id,
            font,
            textBrush,
            new RectangleF((float) el.BBox.Left, (float) el.BBox.Bottom,
                           (float) el.BBox.Width, (float) el.BBox.Height),
            stringFormat);
        }
      }

      // Edges
      foreach (var edge in graph.Edges)
      {
        ICurve curve = edge.Curve;
        var c = curve as Curve;
        if (c != null)
        {
          foreach (ICurve s in c.Segs)
          {
            var l = s as LineSeg;
            if (l != null)
            {
              graphics.DrawLine(edgePen, GleePointToDrawingPoint(l.Start), GleePointToDrawingPoint(l.End));
            }
            var cs = s as CubicBezierSeg;
            if (cs != null)
            {
              graphics.DrawBezier(edgePen, GleePointToDrawingPoint(cs.B(0)), GleePointToDrawingPoint(cs.B(1)), GleePointToDrawingPoint(cs.B(2)), GleePointToDrawingPoint(cs.B(3)));
            }
          }
          if (edge.ArrowHeadAtSource)
          {
            DrawArrow(edge, edgePen, graphics, edge.Curve.Start, edge.ArrowHeadAtSourcePosition);
          }
          if (edge.ArrowHeadAtTarget)
          {
            DrawArrow(edge, edgePen, graphics, edge.Curve.End, edge.ArrowHeadAtTargetPosition);
          }
          if (c.Segs.Count % 2 == 0)
          {
            // Even segs            
            var index = c.Segs.Count/2;
            var l = c.Segs[index] as LineSeg;
            if (l != null)
            {
              var p = GleePointToDrawingPoint(l.End);
              DrawEdgeLabel(graphics, edge, p.X, p.Y);
            }
            var cs = c.Segs[index] as CubicBezierSeg;
            if (cs != null)
            {
              var p = GleePointToDrawingPoint(cs.B(2));
              DrawEdgeLabel(graphics, edge, p.X, p.Y);
            }
          }
          else
          {
            // Uneven segs
            var index = c.Segs.Count / 2;
            var l = c.Segs[index] as LineSeg;
            if (l != null)
            {
              var p1 = GleePointToDrawingPoint(l.Start);
              var p2 = GleePointToDrawingPoint(l.End);
              DrawEdgeLabel(graphics, edge, (p2.X - p1.X) / 2, (p2.Y - p1.Y) / 2);
            }
            var cs = c.Segs[index] as CubicBezierSeg;
            if (cs != null)
            {
              var p = GleePointToDrawingPoint(cs.B(1));
              DrawEdgeLabel(graphics, edge, p.X, p.Y);
            }
          }
        }
      }
      graphics.Flush();
    }

    private static void DrawEdgeLabel(Graphics graphics, Edge edge, float x, float y)
    {
      graphics.DrawString(
        (string)edge.UserData,
        edgeFont,
        edgeTextBrush,
        new PointF(x, y),
        stringFormat);
    }

    private static void DrawArrow(Edge e, Pen pen, Graphics graphics, P start, P end)
    {
      PointF[] points;
      float arrowAngle = 30;
      P dir = end - start;
      P h = dir;
      dir /= dir.Length;
      P s = new P(-dir.Y, dir.X);
      s *= h.Length * ((float)Math.Tan(arrowAngle * 0.5f * (Math.PI / 180.0)));
      points = new PointF[] { GleePointToDrawingPoint(start + s), GleePointToDrawingPoint(end), GleePointToDrawingPoint(start - s) };
      graphics.FillPolygon(pen.Brush, points);
    }

    private static System.Drawing.Point GleePointToDrawingPoint(Microsoft.Glee.Splines.Point point)
    {
      return new System.Drawing.Point((int)point.X, (int)point.Y);
    }

    private static GleeGraph CreateAndLayoutGraph(string fileName)
    {
      // Read the file as one string
      var fileString = string.Empty;
      using (var myFile = new StreamReader(fileName))
      {
        fileString = myFile.ReadToEnd();
      }

      var xmlParser = new XmlParser(fileString);

      startNode = xmlParser.StartState;

      var graph = new GleeGraph {Margins = margin};

      foreach (var state in xmlParser.States)
      {
        graph.AddNode(new Node(state, new Ellipse(nodeWidth, nodeHeight, new P())));
      }

      foreach (var transition in xmlParser.Transitions)
      {
        var e = new Edge(graph.NodeMap[transition.From], graph.NodeMap[transition.To]) {UserData = transition.Trigger};
        graph.AddEdge(e);
      }

      graph.CalculateLayout();
      return graph;
    }
  }
}
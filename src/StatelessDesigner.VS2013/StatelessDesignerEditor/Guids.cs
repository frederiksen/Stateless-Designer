// Guids.cs
// MUST match guids.h
using System;

namespace mrtn.StatelessDesignerEditor
{
    static class GuidList
    {
        public const string guidStatelessDesignerEditorPkgString = "12ef88de-3c91-4dab-a581-d0f96f9c45c7";
        public const string guidStatelessDesignerEditorCmdSetString = "1b9fde94-68bc-46cd-bfd9-bff41d7d5a4f";
        public const string guidStatelessDesignerEditorEditorFactoryString = "d5d9fb60-96a6-44b0-9534-f02591879e7b";

        public static readonly Guid guidStatelessDesignerEditorCmdSet = new Guid(guidStatelessDesignerEditorCmdSetString);
        public static readonly Guid guidStatelessDesignerEditorEditorFactory = new Guid(guidStatelessDesignerEditorEditorFactoryString);
    };
}
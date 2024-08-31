using System;

namespace UmbrellaToolsKit.Storage
{
    public struct SaveSettings
    {
        public ISaveIntegration SaveIntegration;
        public string FilePath;
    }
}

using BlogProject.Business.Tools.LogTool;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.Tools.FacadeTool
{
    public class Facade : IFacade
    {
        public IMemoryCache MemoryCache { get; set; }
        public ICustomLogger CustomLogger { get; set; }

        public Facade(IMemoryCache memoryCache, ICustomLogger customLogger)
        {
            MemoryCache = memoryCache;
            CustomLogger = customLogger;
        }
    }
}

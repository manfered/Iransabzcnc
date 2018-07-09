using dotless.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Iransabzcnc.WEB.App_Start
{
    public class LessTransform : IBundleTransform
    {
        private string _path;

        public LessTransform(string path)
        {
            _path = path;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            Directory.SetCurrentDirectory(_path);

            response.Content = Less.Parse(response.Content);
            response.ContentType = "text/css";
        }
    }
}
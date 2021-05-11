using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValheimJsonExporter.Docs
{
    public class RPCDoc : Doc
    {
        public RPCDoc() : base("ValheimJsonExporter/Docs/rpc-list.json")
        {
            Save();
        }
    }
}

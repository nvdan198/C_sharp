﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Microsoft.SCP;
using Microsoft.SCP.Rpc.Generated;

namespace BTL
{
    public class BatchBolt1 : ISCPBatchBolt
    {
        public void Execute(SCPTuple tuple)
        {
        }

        public void FinishBatch(Dictionary<string, Object> parms)
        {
        }
    }
}
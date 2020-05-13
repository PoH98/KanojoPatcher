using DesktopKanojoPatcher;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesktopKanajoPatcher
{
    public class PatchFPS : Patch
    {
        public int FPS =  60;
        public override void Apply(Kanojo Kanojo)
        {
            var types = Kanojo.AssemblyCSharp.Find("GameManager", true);
            var start = types.FindMethod("Start");
            var body = start.Body;
            bool completed = false;
            foreach(var ins in body.Instructions)
            {
                if (ins.OpCode == OpCodes.Ldc_I4_S)
                {
                    ins.Operand = (sbyte)FPS;
                    completed = true;
                    Console.WriteLine("Patched Operand as " + ins.Operand);
                }
            }
            Console.WriteLine("FPS Patch success: " + completed);
        }
    }

    public class PatchMovement : Patch
    {
        public int[] Sys { get; set; } = new int[] { 30, 60 };
        public override void Apply(Kanojo Kanojo)
        {
            var types = Kanojo.AssemblyCSharp.Find("AnimationManager", true);
            var update = types.FindMethod("Update");
            bool completed = false;
            var body = update.Body;
            var index = 0;
            foreach(var ins in body.Instructions)
            {
                if(ins.OpCode == OpCodes.Ldc_R4)
                {
                    try
                    {
                        ins.Operand = (float)Sys[index];
                        index++;
                        
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            completed = true;
            Console.WriteLine("Movement Time Delay Patch success: " + completed);
        }
    }

    public class 
}

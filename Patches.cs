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
        public int[] Sys { get; set; }
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
            /*
            List<Instruction> realInstructions = new List<Instruction>();
            foreach(var ins in Instructions.structure)
            {
                if (ins.Operand != null)
                {
                    if (ins.Operand.ToString().Contains("IL_"))
                    {
                        ins.Operand = Instructions.structure.Where(x => x.Offset == Convert.ToInt32(ins.Operand.ToString().Replace("IL_", "").Substring(0, 4), 16)).First();
                    }
                    else if(ins.Operand.ToString().Contains("::"))
                    {
                        var ori = body.Instructions.Where(x => x.Operand != null).Where(x => x.Operand.ToString() == ins.Operand.ToString() && x.OpCode == ins.OpCode);
                        if (ori.Count() > 0)
                        {
                            ins.Operand = ori.First().Operand;
                        }
                        else
                        {
                            if(ins.Operand.ToString().Contains("::"))
                            {
                                var operandString = ins.Operand.ToString();
                                var typeName = operandString.Substring(operandString.IndexOf(' ')).Remove(operandString.Substring(operandString.IndexOf(' ')).IndexOf("::")).Trim();
                                if (typeName.Contains("."))
                                    typeName = typeName.Substring(typeName.LastIndexOf('.')+1);
                                var type = Kanojo.UnityEngine.Find(typeName, true);
                                if(type == null)
                                {
                                    type = Kanojo.AssemblyCSharp.Find(typeName, true);
                                }
                                if(type == null)
                                {
                                    var containsName = operandString.Substring(operandString.IndexOf("::") + 2);
                                    if (containsName.Contains("("))
                                        containsName = containsName.Substring(0, containsName.IndexOf("("));
                                    var oriResult = body.Instructions.Where(x=>x.Operand != null).Where(x => x.Operand.ToString().Contains(typeName) && x.Operand.ToString().Contains(containsName));
                                    if(oriResult.Count() > 0)
                                    {
                                        ins.Operand = oriResult.First().Operand;
                                        goto Skip;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error parse " + ins.Operand.ToString());
                                        Console.WriteLine("FPS Patch success: " + completed);
                                        return;
                                    }
                                }

                                var methodName = operandString.Substring(operandString.IndexOf("::")+ 2);
                                if(methodName.Contains("("))
                                    methodName = methodName.Substring(0, methodName.IndexOf("("));
                                object method = type.FindMethod(methodName);
                                if(method == null)
                                {
                                    method = type.FindField(methodName);
                                }
                                if(method == null)
                                {
                                    method = type.FindProperty(methodName);
                                }
                                if(method == null)
                                {
                                    Console.WriteLine("Error parse " + ins.Operand.ToString());
                                    Console.WriteLine("FPS Patch success: " + completed);
                                    return;
                                }
                                ins.Operand = method;
                                Skip:
                                ;
                            }
                        }
                    }
                }
                realInstructions.Add(ins);
            }
            body.Instructions.Clear();
            foreach (var ri in realInstructions)
            {
                body.Instructions.Add(ri);
            }*/
            completed = true;
            Console.WriteLine("Movement Time Delay Patch success: " + completed);
        }
    }

}

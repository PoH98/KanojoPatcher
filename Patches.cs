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
        public override void Apply(Kanojo Kanojo)
        {
            var types = Kanojo.AssemblyCSharp.Find("AnimationManager", true);
            var update = types.FindMethod("Update");
            bool completed = false;
            /*var RandDelay = 15/randomAnim.Length* Random.Range(39000, 41000); */
            int r4Count = 0;
            var body = update.Body;
            Instruction[] structure = new Instruction[]
            {
               new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 0
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Boolean AnimationManager::get_defaltIdleState()",
Offset = 1
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_0044: ldarg.0",
Offset = 6
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 8
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "System.Boolean AnimationManager::waitForRandom",
Offset = 9
},
new Instruction
{
OpCode = OpCodes.Brtrue_S,
Operand = "IL_0044: ldarg.0",
Offset = 14
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 16
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_1,
Offset = 17
},
new Instruction
{
OpCode = OpCodes.Stfld,
Operand = "System.Boolean AnimationManager::waitForRandom",
Offset = 18
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 23
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Single UnityEngine.Time::get_time()",
Offset = 24
},
new Instruction
{
OpCode = OpCodes.Stfld,
Operand = "System.Single AnimationManager::waitStartTime",
Offset = 29
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 34
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_S,
Operand = (sbyte)10,
Offset = 35
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 37
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "UnityEngine.AnimationClip[] AnimationManager::randomAnim",
Offset = 38
},
new Instruction
{
OpCode = OpCodes.Ldlen,
Offset = 43
},
new Instruction
{
OpCode = OpCodes.Conv_I4,

Offset = 44
},
new Instruction
{
OpCode = OpCodes.Div,
Offset = 45
},
new Instruction
{
OpCode = OpCodes.Ldc_R4,
Operand = 20f,
Offset = 46
},
new Instruction
{
OpCode = OpCodes.Ldc_R4,
Operand = 24f,
Offset = 51
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Int32 UnityEngine.Random::Range(System.Int32,System.Int32)",
Offset = 56
},
new Instruction
{
OpCode = OpCodes.Mul,
Offset = 61
},
new Instruction
{
OpCode = OpCodes.Conv_R4,
Offset = 62
},
new Instruction
{
OpCode = OpCodes.Stfld,
Operand = "System.Single AnimationManager::waitDuration",
Offset = 63
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 68
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Boolean AnimationManager::get_defaltIdleState()",
Offset = 69
},
new Instruction
{
OpCode = OpCodes.Brtrue_S,
Operand = "IL_005B: ldarg.0",
Offset = 74
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 76
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "System.Boolean AnimationManager::waitForRandom",
Offset = 77
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_005B: ldarg.0",
Offset = 82
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,

Offset = 84
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_0,
Offset = 85
},
new Instruction
{
OpCode = OpCodes.Stfld,
Operand = "System.Boolean AnimationManager::waitForRandom",
Offset = 86
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 91
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "System.Boolean AnimationManager::waitForRandom",
Offset = 92
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_00A2: ldarg.0",
Offset = 97
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 99
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Boolean AnimationManager::get_isIdleState()",
Offset = 100
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_009B: ldarg.0",
Offset = 105
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 107
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "VoiceController AnimationManager::vc",
Offset = 108
},
new Instruction
{
OpCode = OpCodes.Callvirt,
Operand = "System.Boolean VoiceController::get_CanSpeak()",
Offset = 113
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_009B: ldarg.0",
Offset = 118
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Single UnityEngine.Time::get_time()",
Offset = 120
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 125
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "System.Single AnimationManager::waitStartTime",
Offset = 126
},
new Instruction
{
OpCode = OpCodes.Sub,
Offset = 131
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 132
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "System.Single AnimationManager::waitDuration",
Offset = 133
},
new Instruction
{
OpCode = OpCodes.Ble_Un_S,
Operand = "IL_00A2: ldarg.0",
Offset = 138
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 140
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Void AnimationManager::PlayRandomAnim()",
Offset = 141
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,

Offset = 146
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_0,

Offset = 147
},
new Instruction
{
OpCode = OpCodes.Stfld,
Operand = "System.Boolean AnimationManager::waitForRandom",
Offset = 148
},
new Instruction
{
OpCode = OpCodes.Br_S,
Operand = "IL_00A2: ldarg.0",
Offset = 153
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 155
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_0,

Offset = 156
},
new Instruction
{
OpCode = OpCodes.Stfld,
Operand = "System.Boolean AnimationManager::waitForRandom",
Offset = 157
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 162
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "System.Boolean AnimationManager::waitForExit",
Offset = 163
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_00CB: ldc.i4.s 49",
Offset = 168
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Single UnityEngine.Time::get_time()",
Offset = 170
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 175
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "System.Single AnimationManager::startWaitForExitTime",
Offset = 176
},
new Instruction
{
OpCode = OpCodes.Sub,

Offset = 181
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 182
},
new Instruction
{
OpCode = OpCodes.Ldfld,
Operand = "System.Single AnimationManager::waitForExitDuration",
Offset = 183
},
new Instruction
{
OpCode = OpCodes.Ble_Un_S,
Operand = "IL_00CB: ldc.i4.s 49",
Offset = 188
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 190
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_0,
Offset = 191
},
new Instruction
{
OpCode = OpCodes.Stfld,
Operand = "System.Boolean AnimationManager::waitForExit",
Offset = 192
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 197
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Void AnimationManager::PlayDefaultAnim()",
Offset = 198
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_S,
Operand = (sbyte)49,
Offset = 203
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Boolean UnityEngine.Input::GetKeyDown(UnityEngine.KeyCode)",
Offset = 205
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_00DC: ldc.i4.s 50",
Offset = 210
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 212
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_0,
Offset = 213
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Void AnimationManager::TestAnim(System.Int32)",
Offset = 214
},
new Instruction
{
OpCode = OpCodes.Ret,
Offset = 219
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_S,
Operand = (sbyte)50,
Offset = 220
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Boolean UnityEngine.Input::GetKeyDown(UnityEngine.KeyCode)",
Offset = 222
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_00ED: ldc.i4.s 51",
Offset = 227
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 229
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_1,
Offset = 230
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Void AnimationManager::TestAnim(System.Int32)",
Offset = 231
},
new Instruction
{
OpCode = OpCodes.Ret,
Offset = 236
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_S,
Operand = (sbyte)51,
Offset = 237
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Boolean UnityEngine.Input::GetKeyDown(UnityEngine.KeyCode)",
Offset = 239
},
new Instruction
{
OpCode = OpCodes.Brfalse_S,
Operand = "IL_00FD: ret",
Offset = 244
},
new Instruction
{
OpCode = OpCodes.Ldarg_0,
Offset = 246
},
new Instruction
{
OpCode = OpCodes.Ldc_I4_2,
Offset = 247
},
new Instruction
{
OpCode = OpCodes.Call,
Operand = "System.Void AnimationManager::TestAnim(System.Int32)",
Offset = 248
},
new Instruction
{
OpCode = OpCodes.Ret,
Offset = 253
},
            };
            List<Instruction> realInstructions = new List<Instruction>();
            foreach(var ins in structure)
            {
                if (ins.Operand != null)
                {
                    if (ins.Operand.ToString().Contains("IL_"))
                    {
                        ins.Operand = structure.Where(x => x.Offset == Convert.ToInt32(ins.Operand.ToString().Replace("IL_", "").Substring(0, 4), 16)).First();
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
            }
            completed = true;
            Console.WriteLine("Movement Time Delay Patch success: " + completed);
        }
    }

}

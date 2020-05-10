using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopKanajoPatcher
{
   public class Instructions
    {
      public static readonly Instruction[] structure = new Instruction[]
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
    }
}

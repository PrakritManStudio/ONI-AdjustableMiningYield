using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using PeterHan.PLib.Database;
using PeterHan.PLib.Options;

namespace AdjustableMiningYield
{
    public class AdjustableMiningYield : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new PLocalization().Register();
            var options = new POptions();
            options.RegisterOptions(this, typeof(AdjustableMiningYieldConfig));
        }

        [HarmonyPatch(typeof(WorldDamage), "OnDigComplete")]
        internal class AdjustableMiningYield_WorldDamage_OnDigComplete
        {
            
            private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var config = POptions.ReadSettings<AdjustableMiningYieldConfig>() ?? new AdjustableMiningYieldConfig();
                foreach (CodeInstruction instruction in instructions.ToList())
                {
                    if (IsInstructionLoadConstant(instruction, 0.5f))
                    {
                        Debug.Log("=== Transpiler applied ===");

                        float yValue;
                        switch (config.YieldValue)
                        {
                            case DuplicantSightRange.V35:
                                yValue = 0.35f;
                                break;
                            case DuplicantSightRange.V50:
                                yValue = 0.5f;
                                break;
                            case DuplicantSightRange.V55:
                                yValue = 0.55f;
                                break;
                            case DuplicantSightRange.V60:
                                yValue = 0.6f;
                                break;
                            case DuplicantSightRange.V75:
                                yValue = 0.75f;
                                break;
                            case DuplicantSightRange.V100:
                                yValue = 1.0f;
                                break;
                            case DuplicantSightRange.V125:
                                yValue = 1.25f;
                                break;
                            case DuplicantSightRange.V150:
                                yValue = 1.5f;
                                break;
                            default:
                                yValue = 0.5f;
                                break;

                        }
                        instruction.operand = yValue;
                    }

                    yield return instruction;
                }
            }

            private static bool IsInstructionLoadConstant(CodeInstruction instruction, float value)
            {
                return instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == value;
            }
        }
    }
}

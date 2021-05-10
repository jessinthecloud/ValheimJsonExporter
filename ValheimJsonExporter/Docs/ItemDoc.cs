using System;
using Jotunn.Managers;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using BepInEx;

namespace ValheimJsonExporter.Docs
{
    public class ItemDoc : Doc
    {
        public ItemDoc() : base("ValheimJsonExporter/Docs/conceptual/objects/item-list.json")
        {
            ItemManager.OnItemsRegistered += DocItems;
        }

        private void DocItems()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("-------VALHEIM JSON EXPORTER Documenting items----");

            // create array to hold all of the item objects
            SimpleJson.JsonArray jsonInfo = new SimpleJson.JsonArray();

            foreach (GameObject obj in ObjectDB.instance.m_items)
            {
                // get item drop data
                ItemDrop item = obj.GetComponent<ItemDrop>();
                ItemDrop.ItemData.SharedData shared = item.m_itemData.m_shared;

                // create object to hold the item shared data
                SimpleJson.JsonObject jsonInfoObj = new SimpleJson.JsonObject();

                // populate the item data 
                jsonInfoObj.Add("raw_name", ValheimJsonExporter.Localize(shared.m_name)); 
                jsonInfoObj.Add("var_name", shared.m_name); 
                // no true_name for shared data

                jsonInfoObj.Add("item_type_name", shared.m_itemType.ToString()); // ItemType
                jsonInfoObj.Add("item_type", shared.m_itemType); // ItemType
                jsonInfoObj.Add("description", ValheimJsonExporter.Localize(shared.m_description));
                jsonInfoObj.Add("ai_attack_interval", shared.m_aiAttackInterval);
                jsonInfoObj.Add("ai_attack_max_angle", shared.m_aiAttackMaxAngle);
                jsonInfoObj.Add("ai_attack_range", shared.m_aiAttackRange);
               jsonInfoObj.Add("ai_attack_range_min", shared.m_aiAttackRangeMin);
                jsonInfoObj.Add("ai_prioritized", shared.m_aiPrioritized);
                jsonInfoObj.Add("ai_target_type", shared.m_aiTargetType); // AiTarget
                jsonInfoObj.Add("ai_when_flying", shared.m_aiWhenFlying);
                jsonInfoObj.Add("ai_when_swiming", shared.m_aiWhenSwiming);
                jsonInfoObj.Add("ai_when_walking", shared.m_aiWhenWalking);
                jsonInfoObj.Add("animation_state", shared.m_animationState); // AnimationState
                jsonInfoObj.Add("armor", shared.m_armor);
                jsonInfoObj.Add("attack_force", shared.m_attackForce);
                jsonInfoObj.Add("backstab_bonus", shared.m_backstabBonus);
                jsonInfoObj.Add("blockable", shared.m_blockable);
                jsonInfoObj.Add("block_power", shared.m_blockPower);
                jsonInfoObj.Add("block_power_per_level", shared.m_blockPowerPerLevel);
                jsonInfoObj.Add("can_be_repaired", shared.m_canBeReparied);
                jsonInfoObj.Add("deflection_force", shared.m_deflectionForce);
                jsonInfoObj.Add("deflection_force_per_level", shared.m_deflectionForcePerLevel);
                jsonInfoObj.Add("destroy_broken", shared.m_destroyBroken);
                jsonInfoObj.Add("dodgeable", shared.m_dodgeable);
                jsonInfoObj.Add("durability_drain", shared.m_durabilityDrain);
                jsonInfoObj.Add("durability_per_level", shared.m_durabilityPerLevel);
                jsonInfoObj.Add("equip_duration", shared.m_equipDuration);
                jsonInfoObj.Add("food", shared.m_food);
                jsonInfoObj.Add("food_burn_time", shared.m_foodBurnTime);
                jsonInfoObj.Add("food_regen", shared.m_foodRegen);
                jsonInfoObj.Add("food_stamina", shared.m_foodStamina);
                jsonInfoObj.Add("helmet_hide_hair", shared.m_helmetHideHair);
                jsonInfoObj.Add("hold_duration_min", shared.m_holdDurationMin);
                jsonInfoObj.Add("hold_stamina_drain", shared.m_holdStaminaDrain);
                jsonInfoObj.Add("max_durability", shared.m_maxDurability);
                jsonInfoObj.Add("max_quality", shared.m_maxQuality);
                jsonInfoObj.Add("max_stack_size", shared.m_maxStackSize);
                jsonInfoObj.Add("movement_modifier", shared.m_movementModifier);
                jsonInfoObj.Add("quest_item", shared.m_questItem);
                jsonInfoObj.Add("set_size", shared.m_setSize);
                jsonInfoObj.Add("teleportable", shared.m_teleportable);
                jsonInfoObj.Add("timed_block_bonus", shared.m_timedBlockBonus);
                jsonInfoObj.Add("tool_tier", shared.m_toolTier);
                jsonInfoObj.Add("use_durability", shared.m_useDurability);
                jsonInfoObj.Add("use_durability_drain", shared.m_useDurabilityDrain);
                jsonInfoObj.Add("value", shared.m_value);
                jsonInfoObj.Add("variants", shared.m_variants);
                jsonInfoObj.Add("weight", shared.m_weight);
                jsonInfoObj.Add("ammo_type", shared.m_ammoType);
                jsonInfoObj.Add("damages", shared.m_damages); // HitData.DamageTypes
                jsonInfoObj.Add("damages_per_level", shared.m_damagesPerLevel); // HitData.DamageTypes
                // jsonInfoObj.Add("damage_modifiers", shared.m_damageModifiers); // List<HitData.DamageModPair>
                jsonInfoObj.Add("skill_type", shared.m_skillType); // Skills.SkillType
                // jsonInfoObj.Add("armor_material", shared.m_armorMaterial.ToString()); // Material
               
                  jsonInfoObj.Add("attack", shared.m_attack.m_attackType); // Attack 
                  jsonInfoObj.Add("secondary_attack", shared.m_secondaryAttack.m_attackType); // Attack

                SimpleJson.JsonArray statusEffects = new SimpleJson.JsonArray();

                if (shared.m_attackStatusEffect)
                {
                    SimpleJson.JsonObject jsonStatusEffect = new SimpleJson.JsonObject();
                    jsonStatusEffect.Add("type", "attack");
                    jsonStatusEffect.Add("var_name", shared.m_attackStatusEffect.m_name);
                    jsonStatusEffect.Add("raw_name", ValheimJsonExporter.Localize(shared.m_attackStatusEffect.m_name));
                    jsonStatusEffect.Add("true_name", shared.m_attackStatusEffect.name);
                    statusEffects.Add(jsonStatusEffect);
                }
                if (shared.m_consumeStatusEffect)
                {
                    SimpleJson.JsonObject jsonStatusEffect = new SimpleJson.JsonObject();
                    jsonStatusEffect.Add("type", "consume");
                    jsonStatusEffect.Add("var_name", shared.m_consumeStatusEffect.m_name);
                    jsonStatusEffect.Add("raw_name", ValheimJsonExporter.Localize(shared.m_consumeStatusEffect.m_name));
                    jsonStatusEffect.Add("true_name", shared.m_consumeStatusEffect.name);
                    statusEffects.Add(jsonStatusEffect);
                }
                if (shared.m_equipStatusEffect)
                {
                    SimpleJson.JsonObject jsonStatusEffect = new SimpleJson.JsonObject();
                    jsonStatusEffect.Add("type", "equip");
                    jsonStatusEffect.Add("var_name", shared.m_equipStatusEffect.m_name);
                    jsonStatusEffect.Add("raw_name", ValheimJsonExporter.Localize(shared.m_equipStatusEffect.m_name));
                    jsonStatusEffect.Add("true_name", shared.m_equipStatusEffect.name);
                    statusEffects.Add(jsonStatusEffect);
                }
                if (shared.m_setStatusEffect)
                {
                    SimpleJson.JsonObject jsonStatusEffect = new SimpleJson.JsonObject();
                    jsonStatusEffect.Add("type", "set");
                    jsonStatusEffect.Add("var_name", shared.m_setStatusEffect.m_name);
                    jsonStatusEffect.Add("raw_name", ValheimJsonExporter.Localize(shared.m_setStatusEffect.m_name));
                    jsonStatusEffect.Add("true_name", shared.m_setStatusEffect.name);
                    statusEffects.Add(jsonStatusEffect);
                }

                jsonInfoObj.Add("status_effects", statusEffects);

                // Object to hold info about a single item
                SimpleJson.JsonObject jsonObj = new SimpleJson.JsonObject();
                // item name
                jsonObj.Add("var_name", shared.m_name);
                jsonObj.Add("raw_name", ValheimJsonExporter.Localize(shared.m_name));
                jsonObj.Add("true_name", obj.name);
                // item data
                jsonObj.Add("shared_data", jsonInfoObj);
                // add item to the overall array
                jsonInfo.Add(jsonObj);

            }
            // write to the file
            AddText(jsonInfo.ToString());


            Save();
        }
    }
}

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

            Jotunn.Logger.LogInfo("VALHEIM JSON EXPORTER Documenting items");

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
                jsonInfoObj.Add("name", ValheimJsonExporter.Localize(shared.m_name));
                jsonInfoObj.Add("itemType", shared.m_itemType.ToString()); // ItemType
                jsonInfoObj.Add("description", ValheimJsonExporter.Localize(shared.m_description));
                jsonInfoObj.Add("aiAttackInterval", shared.m_aiAttackInterval);
                jsonInfoObj.Add("aiAttackMaxAngle", shared.m_aiAttackMaxAngle);
                jsonInfoObj.Add("aiAttackRange", shared.m_aiAttackRange);
                // jsonInfoObj.Add("aiAttackRangeMin", shared.m_aiAttackRangeMin); // null
                // jsonInfoObj.Add("aiPrioritized", shared.m_aiPrioritized); // null
                // jsonInfoObj.Add("aiTargetType", shared.m_aiTargetType); // AiTarget
                jsonInfoObj.Add("aiWhenFlying", shared.m_aiWhenFlying);
                jsonInfoObj.Add("aiWhenSwiming", shared.m_aiWhenSwiming);
                jsonInfoObj.Add("aiWhenWalking", shared.m_aiWhenWalking);
                jsonInfoObj.Add("animationState", shared.m_animationState); // AnimationState
                jsonInfoObj.Add("armor", shared.m_armor);
                jsonInfoObj.Add("attackForce", shared.m_attackForce);
                jsonInfoObj.Add("backstabBonus", shared.m_backstabBonus);
                jsonInfoObj.Add("blockable", shared.m_blockable);
                jsonInfoObj.Add("blockPower", shared.m_blockPower);
                jsonInfoObj.Add("blockPowerPerLevel", shared.m_blockPowerPerLevel);
                jsonInfoObj.Add("canBeReparied", shared.m_canBeReparied);
                jsonInfoObj.Add("deflectionForce", shared.m_deflectionForce);
                jsonInfoObj.Add("deflectionForcePerLevel", shared.m_deflectionForcePerLevel);
                jsonInfoObj.Add("destroyBroken", shared.m_destroyBroken);
                jsonInfoObj.Add("dodgeable", shared.m_dodgeable);
                jsonInfoObj.Add("durabilityDrain", shared.m_durabilityDrain);
                jsonInfoObj.Add("durabilityPerLevel", shared.m_durabilityPerLevel);
                jsonInfoObj.Add("equipDuration", shared.m_equipDuration);
                jsonInfoObj.Add("food", shared.m_food);
                jsonInfoObj.Add("foodBurnTime", shared.m_foodBurnTime);
                jsonInfoObj.Add("foodRegen", shared.m_foodRegen);
                jsonInfoObj.Add("foodStamina", shared.m_foodStamina);
                jsonInfoObj.Add("helmetHideHair", shared.m_helmetHideHair);
                jsonInfoObj.Add("holdDurationMin", shared.m_holdDurationMin);
                jsonInfoObj.Add("holdStaminaDrain", shared.m_holdStaminaDrain);
                jsonInfoObj.Add("maxDurability", shared.m_maxDurability);
                jsonInfoObj.Add("maxQuality", shared.m_maxQuality);
                jsonInfoObj.Add("maxStackSize", shared.m_maxStackSize);
                jsonInfoObj.Add("movementModifier", shared.m_movementModifier);
                jsonInfoObj.Add("questItem", shared.m_questItem);
                jsonInfoObj.Add("setSize", shared.m_setSize);
                jsonInfoObj.Add("teleportable", shared.m_teleportable);
                jsonInfoObj.Add("timedBlockBonus", shared.m_timedBlockBonus);
                jsonInfoObj.Add("toolTier", shared.m_toolTier);
                jsonInfoObj.Add("useDurability", shared.m_useDurability);
                jsonInfoObj.Add("useDurabilityDrain", shared.m_useDurabilityDrain);
                jsonInfoObj.Add("value", shared.m_value);
                jsonInfoObj.Add("variants", shared.m_variants);
                jsonInfoObj.Add("weight", shared.m_weight);
                jsonInfoObj.Add("ammoType", shared.m_ammoType);
                // jsonInfoObj.Add("damages", shared.m_damages); // HitData.DamageTypes
                // jsonInfoObj.Add("damagesPerLevel", shared.m_damagesPerLevel); // HitData.DamageTypes
                // jsonInfoObj.Add("damageModifiers", shared.m_damageModifiers); // List<HitData.DamageModPair>
                // jsonInfoObj.Add("skillType", shared.m_skillType); // Skills.SkillType
                // jsonInfoObj.Add("armorMaterial", shared.m_armorMaterial); // Material
                // jsonInfoObj.Add("attack", shared.m_attack); // Attack
                // jsonInfoObj.Add("secondaryAttack", shared.m_secondaryAttack); // Attack
                if (shared.m_attackStatusEffect)
                {
                    jsonInfoObj.Add("attackStatusEffect", shared.m_attackStatusEffect.name);
                }
                if (shared.m_consumeStatusEffect)
                {
                    jsonInfoObj.Add("consumeStatusEffect", shared.m_consumeStatusEffect.name);
                }
                if (shared.m_equipStatusEffect)
                {
                    jsonInfoObj.Add("equipStatusEffect", shared.m_equipStatusEffect.name);
                }
                if (shared.m_setStatusEffect)
                {
                    jsonInfoObj.Add("setStatusEffect", shared.m_setStatusEffect.name);
                }

                // Object to hold info about a single item
                SimpleJson.JsonObject jsonObj = new SimpleJson.JsonObject();
                // item name
                jsonObj.Add("name", obj.name);
                // item data
                jsonObj.Add("shared", jsonInfoObj);
                // add item to the overall array
                jsonInfo.Add(jsonObj);

            }
            // write to the file
            AddText(jsonInfo.ToString());


            Save();
        }
    }
}

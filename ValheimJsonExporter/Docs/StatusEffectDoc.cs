using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;

namespace ValheimJsonExporter.Docs
{
    public class StatusEffectDoc : Doc
    {
        public StatusEffectDoc() : base("status-effect-list.json")
        {
            ItemManager.OnItemsRegistered += DocStatusEffects;
        }

        public StatusEffectDoc(string file) : base(file)
        {
            ItemManager.OnItemsRegistered += DocCSStatusEffects;
        }

        private void DocStatusEffects()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON EXPORTER Documenting status effects");


            // create array to hold all of the effects
            SimpleJson.JsonArray effects = new SimpleJson.JsonArray();

            Dictionary<string, StatusEffect> statusEffects = new Dictionary<string, StatusEffect>();

            foreach (GameObject obj in ObjectDB.instance.m_items)
            {
                ItemDrop item = obj.GetComponent<ItemDrop>();
                ItemDrop.ItemData.SharedData shared = item.m_itemData.m_shared;

                if (shared.m_attackStatusEffect)
                {
                    statusEffects[shared.m_attackStatusEffect.name] = shared.m_attackStatusEffect;
                }

                if (shared.m_consumeStatusEffect)
                {
                    statusEffects[shared.m_consumeStatusEffect.name] = shared.m_consumeStatusEffect;
                }

                if (shared.m_equipStatusEffect)
                {
                    statusEffects[shared.m_equipStatusEffect.name] = shared.m_equipStatusEffect;
                }

                if (shared.m_setStatusEffect)
                {
                    statusEffects[shared.m_setStatusEffect.name] = shared.m_setStatusEffect;
                }
            }

            foreach (StatusEffect statusEffect in statusEffects.Values)
            {
                // create object to hold the data
                SimpleJson.JsonObject jsonEffect = new SimpleJson.JsonObject();

                jsonEffect.Add("var_name", statusEffect.m_name);
                jsonEffect.Add("raw_name", ValheimJsonExporter.Localize(statusEffect.m_name));
                jsonEffect.Add("true_name", statusEffect.name);
                jsonEffect.Add("category", ValheimJsonExporter.Localize(statusEffect.m_category));
                jsonEffect.Add("tooltip", ValheimJsonExporter.Localize(statusEffect.m_tooltip));
                // StatusAttribute
                jsonEffect.Add("attributes", ValheimJsonExporter.Localize(statusEffect.m_attributes.ToString()));
                jsonEffect.Add("start_message", ValheimJsonExporter.Localize(statusEffect.m_startMessage));
                jsonEffect.Add("stop_message", ValheimJsonExporter.Localize(statusEffect.m_stopMessage));
                jsonEffect.Add("repeat_message", ValheimJsonExporter.Localize(statusEffect.m_repeatMessage));
                jsonEffect.Add("repeat_interval", statusEffect.m_repeatInterval);
                jsonEffect.Add("cooldown", statusEffect.m_cooldown);
                jsonEffect.Add("activation_animation", statusEffect.m_activationAnimation);

                effects.Add(jsonEffect);

            }

            // write to the file
            AddText(effects.ToString());

            Save();
        }

        private void DocCSStatusEffects()
        {
            if (Generated)
            {
                return;
            }

            Jotunn.Logger.LogInfo("JSON EXPORTER Documenting status effects");


            // create array to hold all of the effects
            SimpleJson.JsonArray effects = new SimpleJson.JsonArray();

            Dictionary<string, StatusEffect> statusEffects = new Dictionary<string, StatusEffect>();

            foreach (GameObject obj in ObjectDB.instance.m_items)
            {
                ItemDrop item = obj.GetComponent<ItemDrop>();
                ItemDrop.ItemData.SharedData shared = item.m_itemData.m_shared;

                if (shared.m_attackStatusEffect)
                {
                    statusEffects[shared.m_attackStatusEffect.name] = shared.m_attackStatusEffect;
                }

                if (shared.m_consumeStatusEffect)
                {
                    statusEffects[shared.m_consumeStatusEffect.name] = shared.m_consumeStatusEffect;
                }

                if (shared.m_equipStatusEffect)
                {
                    statusEffects[shared.m_equipStatusEffect.name] = shared.m_equipStatusEffect;
                }

                if (shared.m_setStatusEffect)
                {
                    statusEffects[shared.m_setStatusEffect.name] = shared.m_setStatusEffect;
                }
            }

            foreach (StatusEffect statusEffect in statusEffects.Values)
            {
                // create object to hold the data
                SimpleJson.JsonObject jsonEffect = new SimpleJson.JsonObject();

                jsonEffect.Add("m_name", ValheimJsonExporter.Localize(statusEffect.m_name));
                jsonEffect.Add("m_category", ValheimJsonExporter.Localize(statusEffect.m_category));
                jsonEffect.Add("m_tooltip", ValheimJsonExporter.Localize(statusEffect.m_tooltip));
                // StatusAttribute
                jsonEffect.Add("m_attributes", ValheimJsonExporter.Localize(statusEffect.m_attributes.ToString()));
                jsonEffect.Add("m_startMessage", ValheimJsonExporter.Localize(statusEffect.m_startMessage));
                jsonEffect.Add("m_stopMessage", ValheimJsonExporter.Localize(statusEffect.m_stopMessage));
                jsonEffect.Add("m_repeatMessage", ValheimJsonExporter.Localize(statusEffect.m_repeatMessage));
                jsonEffect.Add("m_repeatInterval", statusEffect.m_repeatInterval);
                jsonEffect.Add("m_cooldown", statusEffect.m_cooldown);
                jsonEffect.Add("m_activationAnimation", statusEffect.m_activationAnimation);

                effects.Add(jsonEffect);
            }

            // write to the file
            AddText(effects.ToString());

            Save();
        }
    }
}

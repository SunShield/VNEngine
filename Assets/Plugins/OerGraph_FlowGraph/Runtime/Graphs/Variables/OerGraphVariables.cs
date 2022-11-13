using System;
using System.Collections.Generic;
using UnityEngine;

namespace OerGraph_FlowGraph.Runtime.Graphs.Variables
{
    [Serializable]
    public abstract class OerGraphVariables
    {
        [SerializeField] protected string GraphPath;
        [SerializeField] protected string BoolVariablesKey;
        [SerializeField] protected string IntVariablesKey;
        [SerializeField] protected string FloatVariablesKey;
        [SerializeField] protected string StringVariablesKey;
        
        private Dictionary<string, bool> _boolVariables;
        public Dictionary<string, bool> BoolVariables => _boolVariables ??= LoadOrCreateVariables<bool>(BoolVariablesKey);

        private Dictionary<string, int> _intVariables;
        public Dictionary<string, int> IntVariables => _intVariables ??= LoadOrCreateVariables<int>(IntVariablesKey);
        
        private Dictionary<string, float> _floatVariables;
        public Dictionary<string, float> FloatVariables => _floatVariables ??= LoadOrCreateVariables<float>(FloatVariablesKey);
        
        private Dictionary<string, string> _stringVariables;
        public Dictionary<string, string> StringVariables => _stringVariables ??= LoadOrCreateVariables<string>(StringVariablesKey);

        protected OerGraphVariables(string graphPath) => GraphPath = graphPath;
        
        // // // // BOOL // // // //
        
        public void AddBoolVariable(string variableKey, bool value)
        {
            if (BoolVariables.ContainsKey(variableKey)) return;
            
            BoolVariables.Add(variableKey, value);
            SaveVariables(BoolVariables, BoolVariablesKey);
        }
        
        public void RemoveBoolVariable(string variableKey)
        {
            if (!BoolVariables.ContainsKey(variableKey)) return;
            
            BoolVariables.Remove(variableKey);
            SaveVariables(BoolVariables, BoolVariablesKey);
        }

        public bool GetBoolVariable(string variableKey) => BoolVariables[variableKey];

        public void SetBoolVariable(string variableKey, bool value)
        {
            BoolVariables[variableKey] = value;
            SaveVariables(BoolVariables, BoolVariablesKey);
        }
        
        // // // // INT // // // //
        
        public void AddIntVariable(string variableKey, int value)
        {
            if (IntVariables.ContainsKey(variableKey)) return;
            
            IntVariables.Add(variableKey, value);
            SaveVariables(IntVariables, IntVariablesKey);
        }
        
        public void RemoveIntVariable(string variableKey)
        {
            if (!IntVariables.ContainsKey(variableKey)) return;
            
            IntVariables.Remove(variableKey);
            SaveVariables(IntVariables, IntVariablesKey);
        }

        public int GetIntVariable(string variableKey) => IntVariables[variableKey];

        public void SetIntVariable(string variableKey, int value)
        {
            IntVariables[variableKey] = value;
            SaveVariables(IntVariables, IntVariablesKey);
        }
        
        // // // // FLOAT // // // //
        
        public void AddFloatVariable(string variableKey, float value)
        {
            if (FloatVariables.ContainsKey(variableKey)) return;
            
            FloatVariables.Add(variableKey, value);
            SaveVariables(FloatVariables, FloatVariablesKey);
        }
        
        public void RemoveFloatVariable(string variableKey)
        {
            if (!FloatVariables.ContainsKey(variableKey)) return;
            
            FloatVariables.Remove(variableKey);
            SaveVariables(FloatVariables, FloatVariablesKey);
        }

        public float GetFloatVariable(string variableKey) => FloatVariables[variableKey];

        public void SetFloatVariable(string variableKey, float value)
        {
            FloatVariables[variableKey] = value;
            SaveVariables(FloatVariables, FloatVariablesKey);
        }
        
        // // // // STRING // // // //
        
        public void AddStringVariable(string variableKey, string value)
        {
            if (StringVariables.ContainsKey(variableKey)) return;
            
            StringVariables.Add(variableKey, value);
            SaveVariables(StringVariables, StringVariablesKey);
        }
        
        public void RemoveStringVariable(string variableKey)
        {
            if (!StringVariables.ContainsKey(variableKey)) return;
            
            StringVariables.Remove(variableKey);
            SaveVariables(StringVariables, StringVariablesKey);
        }

        public string GetStringVariable(string variableKey) => StringVariables[variableKey];

        public void SetStringVariable(string variableKey, string value)
        {
            StringVariables[variableKey] = value;
            SaveVariables(StringVariables, StringVariablesKey);
        }

        private Dictionary<string, TType> LoadOrCreateVariables<TType>(string key)
        {
            var variablesLoadResult = TryLoadVariables<TType>(key);
            return !variablesLoadResult.loadResult 
                ? CreateVariables<TType>(BoolVariablesKey) 
                : variablesLoadResult.variables;
        }

        protected abstract (bool loadResult, Dictionary<string, TType> variables) TryLoadVariables<TType>(string key);
        protected abstract Dictionary<string, TType> CreateVariables<TType>(string key);
        protected abstract void SaveVariables<TType>(Dictionary<string, TType> variables, string key);
    }
}
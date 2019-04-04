using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace ComponentFunTime.Shared
{
    public sealed class SectionData
    {
        private readonly Dictionary<string, RenderFragment> _sections = new Dictionary<string, RenderFragment>();

        public event EventHandler Changed;

        public RenderFragment GetSection(string name, RenderFragment @default = null)
        {
            _sections.TryGetValue(name, out var value);
            return value ?? @default;
        }

        public bool TryGetSection(string name, out RenderFragment value)
        {
            return _sections.TryGetValue(name, out value);
        }

        public void SetSection(string name, RenderFragment content)
        {
            _sections.TryGetValue(name, out var old);
            _sections[name] = content;

            if (old != content)
            {
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Clear()
        {
            _sections.Clear();
        }
    }
}

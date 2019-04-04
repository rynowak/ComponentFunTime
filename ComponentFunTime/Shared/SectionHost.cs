using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ComponentFunTime.Shared
{
    public class SectionHost : CascadingValue<SectionData>, IComponent
    {
        [Parameter] public EventCallback OnChange { get; private set; }

        async Task IComponent.SetParametersAsync(ParameterCollection parameters)
        {
            var old = Value;

            await base.SetParametersAsync(parameters);

            if (old != null && !object.ReferenceEquals(old, Value))
            {
                old.Changed -= OnSectionChanged;
            }

            if (Value != null && !object.ReferenceEquals(old, Value))
            {
                Value.Changed += OnSectionChanged;
            }
        }

        private void OnSectionChanged(object sender, EventArgs e)
        {
            _ = OnChange.InvokeAsync(null);
        }
    }
}

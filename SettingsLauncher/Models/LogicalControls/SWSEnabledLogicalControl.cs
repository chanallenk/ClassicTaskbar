using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsLauncher.Contracts.Models;

namespace SettingsLauncher.Models.LogicalControls;
public class SWSEnabledLogicalControl : LogicalControlBase, ILogical
{
    public string Name => "IsSWSEnabled";

    public bool Inverted
    {
        get;
        set;
    }

    public override bool CanBeEnabled()
    {
        return false;
    }
}

using System;
using UnityEngine;

[Serializable]
public class upgrades
{
    private String upgradename;
    private bool eqipped;
    private float modifier1;
    private float modifier2;
    
    public upgrades(String upgradename, bool eqipped, float modifier1,float modifier2)
    {
     this.upgradename = upgradename;
     this.eqipped = eqipped;
     this.modifier1 = modifier1;
     this.modifier2 = modifier2;
    }

    public void setEqiped(bool eqipped)
    {
        this.eqipped = eqipped;
    }

    public bool getEqipped()
    {
        return eqipped;
    }

    public String getUpgradename()
    {
        return upgradename;
    }
}

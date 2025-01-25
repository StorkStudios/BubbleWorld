using System.Collections.Generic;
using UnityEngine;

public enum TeaBase { EarlGray, Mint, Matcha }
public enum TeaSyroup { Mango, Strawberry, Milk }
public enum TeaJelly { Chocolate, Almond, Banana }
public enum TeaMixage { Light, Medium, Hard }

public class MinigameStart
{
    public readonly TeaBase teaBase;
    public readonly TeaSyroup teaSyroup;
    public readonly TeaSyroup? secondSyroup;
    public readonly List<TeaJelly> teaJellies;
    public readonly TeaMixage teaMixage;

    public MinigameStart(TeaBase teaBase, TeaSyroup teaSyroup, TeaSyroup? secondSyroup, List<TeaJelly> teaJellies, TeaMixage teaMixage)
    {
        this.teaBase = teaBase;
        this.teaSyroup = teaSyroup;
        this.secondSyroup = secondSyroup;
        this.teaJellies = teaJellies;
        this.teaMixage = teaMixage;
    }
}

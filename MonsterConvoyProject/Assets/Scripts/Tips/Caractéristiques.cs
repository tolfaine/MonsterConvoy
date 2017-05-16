using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CaracHumainType { Stuff, Cheveux}

[System.Serializable]
public class CaractMonster
{
    protected static List<CaractMonster> allCaractMonster = new List<CaractMonster>(0);

    public static readonly CaractMonster TENTACULES = new CaractMonster(0, "TENTACULES");
    public static readonly CaractMonster CORNES = new CaractMonster(0, "CORNES");
    public static readonly CaractMonster AILES = new CaractMonster(0, "AILES");
    public static readonly CaractMonster MOUSTACHES = new CaractMonster(0, "MOUSTACHES");
    public static readonly CaractMonster CARAPACE = new CaractMonster(0, "CARAPACE");
    public static readonly CaractMonster EPINES = new CaractMonster(0, "EPINES");
    public static readonly CaractMonster SOURCILS = new CaractMonster(0, "SOURCILS");
    public static readonly CaractMonster CHAPEAU = new CaractMonster(0, "CHAPEAU");

    
    private int nId;
    [SerializeField]
    private string sName;

    private CaractMonster(int id, string name)
    {
        nId = id;
        sName = name;
        if(!allCaractMonster.Contains(this))
            allCaractMonster.Add(this);
    }

    public static CaractMonster GetRandomCarac()
    {
        int randIndex = Random.Range(0, allCaractMonster.Count);
        return allCaractMonster[randIndex];
    }

    public static List<CaractMonster> GetAllCarac()
    {
        return allCaractMonster;
    }


}

[System.Serializable]
public class CaractHumain
{
    protected static List<CaractHumain> allCaractHumain = new List<CaractHumain>(0);

    protected int nId;
    [SerializeField]
    protected string sName;
    protected CaracHumainType type;

   

    protected  CaractHumain(int id, string name)
    {
        nId = id;
        sName = name;

        if (!allCaractHumain.Contains(this))
            allCaractHumain.Add(this);
    }

    public static CaractHumain GetRandomCarac()
    {
        int randIndex = Random.Range(0, allCaractHumain.Count);
        return allCaractHumain[randIndex];
    }

    public static List<CaractHumain> GetAllCarac()
    {
        return allCaractHumain;
    }
}

[System.Serializable]
public class CaractHumainStuff : CaractHumain
{
    public static readonly CaractHumainStuff BOUCLIER = new CaractHumainStuff(0, "BOUCLIER");
    public static readonly CaractHumainStuff HACHE = new CaractHumainStuff(0, "HACHE");
    public static readonly CaractHumainStuff RICHE = new CaractHumainStuff(0, "RICHE");
    public static readonly CaractHumainStuff TOURISTE = new CaractHumainStuff(0, "TOURISTE");
    public static readonly CaractHumainStuff JUPE = new CaractHumainStuff(0, "JUPE");
    public static readonly CaractHumainStuff CAPE = new CaractHumainStuff(0, "CAPE");
    public static readonly CaractHumainStuff BATON = new CaractHumainStuff(0, "BATON");
    public static readonly CaractHumainStuff ARC = new CaractHumainStuff(0, "ARC");

    protected CaractHumainStuff(int id, string name) : base(id,name){
        this.type = CaracHumainType.Stuff;

        if (!allCaractHumain.Contains(this))
            allCaractHumain.Add(this);
    }

    public static List<CaractHumain> GetAllCarac()
    {
        return allCaractHumain;
    }

}

[System.Serializable]
public class CaractHumainCheveux : CaractHumain
{

    public static readonly CaractHumainCheveux BLOND = new CaractHumainCheveux(0, "BLOND");
    public static readonly CaractHumainCheveux BRUN = new CaractHumainCheveux(0, "BRUN");
    public static readonly CaractHumainCheveux ROUX = new CaractHumainCheveux(0, "ROUX");
    public static readonly CaractHumainCheveux CHATAIN = new CaractHumainCheveux(0, "CHATAIN");
    public static readonly CaractHumainCheveux BLANC = new CaractHumainCheveux(0, "BLANC");
    public static readonly CaractHumainCheveux COLORE = new CaractHumainCheveux(0, "COLORE");
    public static readonly CaractHumainCheveux CHAUVE = new CaractHumainCheveux(0, "CHAUVE");
    public static readonly CaractHumainCheveux CASQUE = new CaractHumainCheveux(0, "CASQUE");

    protected CaractHumainCheveux(int id, string name) : base(id,name){
        this.type = CaracHumainType.Cheveux;
        if (!allCaractHumain.Contains(this))
            allCaractHumain.Add(this);
    }

    public static List<CaractHumain> GetAllCarac()
    {
        return allCaractHumain;
    }

}

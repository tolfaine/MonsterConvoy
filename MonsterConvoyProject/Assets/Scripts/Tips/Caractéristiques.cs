using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CaracHumainType { Stuff, Cheveux}

[System.Serializable]
public class CaractMonster
{
    protected static List<CaractMonster> allCaractMonster = new List<CaractMonster>(0);

    public enum _enumCaractMonster { NONE, TENTACULES, CORNES , AILES, MOUSTACHES, CARAPACE, SAC, SOURCILS, CHAPEAU }

    public static readonly CaractMonster NONE = new CaractMonster(0, _enumCaractMonster.NONE);
    public static readonly CaractMonster TENTACULES = new CaractMonster(0, _enumCaractMonster.TENTACULES);
    public static readonly CaractMonster CORNES = new CaractMonster(0, _enumCaractMonster.CORNES);
    public static readonly CaractMonster AILES = new CaractMonster(0, _enumCaractMonster.AILES);
    public static readonly CaractMonster MOUSTACHES = new CaractMonster(0, _enumCaractMonster.MOUSTACHES);
    public static readonly CaractMonster CARAPACE = new CaractMonster(0, _enumCaractMonster.CARAPACE);
    public static readonly CaractMonster SAC = new CaractMonster(0, _enumCaractMonster.SAC);
    public static readonly CaractMonster SOURCILS = new CaractMonster(0, _enumCaractMonster.SOURCILS);
    public static readonly CaractMonster CHAPEAU = new CaractMonster(0, _enumCaractMonster.CHAPEAU);


    [SerializeField]
    public string sName;
    protected int nId;
    public _enumCaractMonster enumCaract;

    [SerializeField]
   // private string sName;

    private CaractMonster(int id, _enumCaractMonster enumCaractNew)
    {
        nId = id;
        enumCaract = enumCaractNew;
        sName = enumCaract.ToString();

        if (!allCaractMonster.Contains(this))
            allCaractMonster.Add(this);
    }

    public static CaractMonster GetRandomCarac()
    {
        int randIndex = Random.Range(1, allCaractMonster.Count+1);
        return allCaractMonster[randIndex];
    }

    public static CaractMonster GetRandomCaracExept(_enumCaractMonster enumMut, _enumCaractMonster enumMut2)
    {
        int randIndex = Random.Range(1, allCaractMonster.Count );

        while(allCaractMonster[randIndex].enumCaract == enumMut || allCaractMonster[randIndex].enumCaract == enumMut2)
        {
            randIndex = Random.Range(1, allCaractMonster.Count );
        }
        return allCaractMonster[randIndex];
    }

    public static List<CaractMonster> GetAllCarac()
    {
        List<CaractMonster> allCarac = allCaractMonster;
        allCarac.RemoveAt(0);
        return allCarac;
    }

    public static CaractMonster GetCaractMonsterOfEnum(_enumCaractMonster IenumCaract)
    {
        int i = 0;
        switch (IenumCaract)
        {
            case _enumCaractMonster.AILES:
                return CaractMonster.AILES;
                break;
            case _enumCaractMonster.CARAPACE:
                return CaractMonster.CARAPACE;
                break;
            case _enumCaractMonster.CHAPEAU:
                return CaractMonster.CHAPEAU;
                break;
            case _enumCaractMonster.CORNES:
                return CaractMonster.CORNES;
                break;
            case _enumCaractMonster.MOUSTACHES:
                return CaractMonster.MOUSTACHES;
                break;
            case _enumCaractMonster.SAC:
                return CaractMonster.SAC;
                break;
            case _enumCaractMonster.SOURCILS:
                return CaractMonster.SOURCILS;
                break;
            case _enumCaractMonster.TENTACULES:
                return CaractMonster.TENTACULES;
                break;
            case _enumCaractMonster.NONE:
                return CaractMonster.NONE;
                break;
        }
        return null;
    }

}

[System.Serializable]
public class CaractHumain
{
    protected static List<CaractHumain> allCaractHumain = new List<CaractHumain>(0);

    protected int nId;
    [SerializeField]
    public string sName;
    public CaracHumainType type;

   

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
    public enum _enumCaractHumainStuff { BOUCLIER, HACHE, RICHE, TOURISTE, JUPE, CAPE, BATON, ARC }

    public static readonly CaractHumainStuff BOUCLIER = new CaractHumainStuff(0, _enumCaractHumainStuff.BOUCLIER);
    public static readonly CaractHumainStuff HACHE = new CaractHumainStuff(0, _enumCaractHumainStuff.HACHE);
    public static readonly CaractHumainStuff RICHE = new CaractHumainStuff(0, _enumCaractHumainStuff.RICHE);
    public static readonly CaractHumainStuff TOURISTE = new CaractHumainStuff(0, _enumCaractHumainStuff.TOURISTE);
    public static readonly CaractHumainStuff JUPE = new CaractHumainStuff(0, _enumCaractHumainStuff.JUPE);
    public static readonly CaractHumainStuff CAPE = new CaractHumainStuff(0, _enumCaractHumainStuff.CAPE);
    public static readonly CaractHumainStuff BATON = new CaractHumainStuff(0, _enumCaractHumainStuff.BATON);
    public static readonly CaractHumainStuff ARC = new CaractHumainStuff(0, _enumCaractHumainStuff.ARC);

    public _enumCaractHumainStuff enumCaract;

    protected CaractHumainStuff(int id, _enumCaractHumainStuff enumCaractNew) : base(id, enumCaractNew.ToString())
    {
        this.type = CaracHumainType.Stuff;
        this.enumCaract = enumCaractNew;

        if (!allCaractHumain.Contains(this))
            allCaractHumain.Add(this);
    }

    public static List<CaractHumain> GetAllCarac()
    {
        return allCaractHumain;
    }

    public static CaractHumainStuff GetCaractHumainOfEnum(_enumCaractHumainStuff IenumCaract)
    {
        switch (IenumCaract)
        {
            case _enumCaractHumainStuff.ARC:
                return CaractHumainStuff.ARC;
                break;
            case _enumCaractHumainStuff.BATON:
                return CaractHumainStuff.BATON;
                break;
            case _enumCaractHumainStuff.BOUCLIER:
                return CaractHumainStuff.BOUCLIER;
                break;
            case _enumCaractHumainStuff.CAPE:
                return CaractHumainStuff.CAPE;
                break;
            case _enumCaractHumainStuff.HACHE:
                return CaractHumainStuff.HACHE;
                break;
            case _enumCaractHumainStuff.JUPE:
                return CaractHumainStuff.JUPE;
                break;
            case _enumCaractHumainStuff.RICHE:
                return CaractHumainStuff.RICHE;
                break;
            case _enumCaractHumainStuff.TOURISTE:
                return CaractHumainStuff.TOURISTE;
                break;
        }
        return null;
    }

  }

[System.Serializable]
public class CaractHumainCheveux : CaractHumain
{
    public enum _enumCaractHumainCheveux { BLOND, BRUN, ROUX, CHATAIN, BLANC, COLORE, CHAUVE, CASQUE }

    public static readonly CaractHumainCheveux BLOND = new CaractHumainCheveux(0, _enumCaractHumainCheveux.BLOND);
    public static readonly CaractHumainCheveux BRUN = new CaractHumainCheveux(0, _enumCaractHumainCheveux.BRUN);
    public static readonly CaractHumainCheveux ROUX = new CaractHumainCheveux(0, _enumCaractHumainCheveux.ROUX);
    public static readonly CaractHumainCheveux CHATAIN = new CaractHumainCheveux(0, _enumCaractHumainCheveux.CHATAIN);
    public static readonly CaractHumainCheveux BLANC = new CaractHumainCheveux(0, _enumCaractHumainCheveux.BLANC);
    public static readonly CaractHumainCheveux COLORE = new CaractHumainCheveux(0, _enumCaractHumainCheveux.COLORE);
    public static readonly CaractHumainCheveux CHAUVE = new CaractHumainCheveux(0, _enumCaractHumainCheveux.CHAUVE);
    public static readonly CaractHumainCheveux CASQUE = new CaractHumainCheveux(0, _enumCaractHumainCheveux.CASQUE);

    public _enumCaractHumainCheveux enumCaract;

    protected CaractHumainCheveux(int id, _enumCaractHumainCheveux enumCaractNew) : base(id, enumCaractNew.ToString())
    {
        this.type = CaracHumainType.Cheveux;
        this.enumCaract = enumCaractNew;
        if (!allCaractHumain.Contains(this))
            allCaractHumain.Add(this);
    }

    public static List<CaractHumain> GetAllCarac()
    {
        return allCaractHumain;
    }

    public static CaractHumainCheveux GetCaractHumainOfEnum(_enumCaractHumainCheveux IenumCaract)
    {
        switch (IenumCaract)
        {
            case _enumCaractHumainCheveux.BLOND:
                return CaractHumainCheveux.BLOND;
                break;
            case _enumCaractHumainCheveux.BRUN:
                return CaractHumainCheveux.BRUN;
                break;
            case _enumCaractHumainCheveux.ROUX:
                return CaractHumainCheveux.ROUX;
                break;
            case _enumCaractHumainCheveux.CHATAIN:
                return CaractHumainCheveux.CHATAIN;
                break;
            case _enumCaractHumainCheveux.BLANC:
                return CaractHumainCheveux.BLANC;
                break;
            case _enumCaractHumainCheveux.COLORE:
                return CaractHumainCheveux.COLORE;
                break;
            case _enumCaractHumainCheveux.CHAUVE:
                return CaractHumainCheveux.CHAUVE;
                break;
            case _enumCaractHumainCheveux.CASQUE:
                return CaractHumainCheveux.CASQUE;
                break;
        }
        return null;

    }

    public static CaractHumainCheveux GetRandomCarac()
    {
        _enumCaractHumainCheveux randEnum = (_enumCaractHumainCheveux)Random.Range(0, System.Enum.GetValues(typeof(_enumCaractHumainCheveux)).Length);
        return GetCaractHumainOfEnum(randEnum);
    }
}

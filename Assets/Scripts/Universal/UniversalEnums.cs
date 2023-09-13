//This file is a collection of enums that are used throughout entire project.


//Neutral(NotApplicable), StronDrium_dominion(Friendly), democratic_Republic_of_Coprita(Main enemy)
public enum ConqForce
{
    NA, SD, RC
};

//FriendlyBase, NoRmal(Nothing), ProHibited, StrongHold, 
public enum NodeSpec
{
    FB, NR, PH, SH
}

//NORmal(nothing out of ordinary)
public enum NodeState
{
    nor
}

/// <summary>
/// Unit type falls into one of three categories.
/// Character : Character that player has full control over.
/// Facility : Different units that serves purposes as gimmicks.
/// Enemy : Enemy that player has to defeat.
/// </summary>
public enum UnitType
{
    Character,
    Facility,
    Enemy
};
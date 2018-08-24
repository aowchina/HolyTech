namespace HolyTech.GameEntity
{
	public enum EntityType{
		Monster = 1,
		Soldier,
		Building,
		Player,
        AltarSoldier,
	}

    public enum ENPCCateChild
    {
        eNPCChild_None = 0,
        eNPCChild_NPC_Per_AtkBuilding, //攻击箭塔1
        eNPCChild_NPC_Per_Bomb,  //炮兵  2
        eNPCChild_SmallMonster, //小野怪 3
        eNPCChild_HugeMonster,  //大野怪 4

        eNPCChild_BUILD_Altar = 10,  //祭坛
        eNPCChild_BUILD_Base,        //基地
        eNPCChild_BUILD_Shop,        //商店
        eNPCChild_BUILD_Tower,       //箭塔

        eNPCChild_BUILD_Summon = 20,

        eNPCCateChild_Ohter,
    };

 
}

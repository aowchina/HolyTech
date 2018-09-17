namespace Thanos
{
    namespace GameData
    {
        public enum ConstEnum
        {
            ErrorCodeInterval = 0x00010000,
            Four_Bytes = 4,
            DefaultNameLen = 30,
            DefaultNickNameLen = 18,
            DefaultPasswordLen = 33,
            Level1Inter = 10000,
        }

        public enum ErrorCodeEnum
        {
            NullMsgHandler = -65492,
            InvalidUserName = -65476,
            //Server model error code.
            FWW_Begin = -(ConstEnum.ErrorCodeInterval * 2),
            NullGateServer = -131062,
            InvalidUserPwd = -131060,
            NullUser = -131055,
            JustInBattle = -131036,
            InvalidBattlePos = -131035,

            TheBattleUserFull = -131034,
            JustNotInBattle = -131032,
            NotAllUserReady = -131021,
            YouAreNotBattleManager = -131020,
            NullBattle = -131013,
            InvalidMapID = -131006,

            ReEnterRoomFail = -130772,
            UserInfoUnComplete = -130998,
            BattlePDWNotMatch = -130979,

            NULLPassWord = -130788,
            AccountTheGame = -130756,

            NULLNickName = -130977,
            HeroNotDead = -130937,
            NotEnoughGold = -130936,
            NoRebornTimes = -130935,
            BattleIsPlaying = -130934,
            AskBuyGoodsFailForBagFull = -130916,
            NickNameCollision = -130909,
            BattleHaveStart = -130903,
            AddBattleFailForLackOfGold = -130902,
            CampNotBalance = -130901,
            AddBattleFailForAllFull = -130872,
            AddBattleFailForUserFull = -130871,
            WarningToSelectHero = -130870,
            NickNameContainInvalideChar = -130863,
            AskTooFrequently = -130832,

            CannotBuygoodsWhenDeath = -130812,
            CannotSelGoodsWhenDeath = -130813,
            AskHaveSend = -130814,//�����ѷ���
            UserInYourBlackList = -130815,//�������ĺ�������
            CounterpartFriendListFull = -130816,//�Է���������
            UserInBlackList = -130817,//�ڶԷ���������
            JustInBlackList = -130820,//�ں�������
            JustInFriendsList = -130821,//�ں���������
            BlackListFull = -130822,// ����������
            FriendsListFull = -130823,// ������������
            BattleClosing = -130790, //����ʱ��ʾս���Ѿ�����
            InvalidBattleID = -131030,//��Ч��ս��ID
            MatchLinkInvalid = -130767,      //��Ϸ�Ѿ���ʼ
            FuryNotEnough = -130943,   //ŭ������
            FuryRuning = -130944,   //�����ͷ�ŭ��

            FWWClient_Begin = -(ConstEnum.ErrorCodeInterval * 3),
            Base_LostNetConnection,
            InvalidGameSate,
            NumLimit,
            CanNotFindSpecifiedGUID,
            InvlaidGameState,
            InvlaidTroops,
            InvlaidWeaponTypeID,
            InvlaidDetonatorType,
            InvalidNum,
            InvlaidMissileTypeID,
            InvlaidTxtResource,
            CanNotFindTheShip,
            CanNotFindTheBuilding,
            CanNotFindTheCity,
            InvalidObjTypeID2,
            InvalidEquipSlotIdx,
            NullRunTimeForceInfo,
            ProjectileEmpty,
            CannotAttackBuilding,
            InvalidProjectileTypeID,
            LaunchCD,
            NullPointer,
            CannotFindTarget,
            NullSpecifiedComponent,
            InvalidTypeID,
            AbsentDollar,
            InvalidGUFS,
            InvalidGameUnitType,
            LoadConfigFileFail,

            ConstInvalid = -2,
            OperatePending = -1,
            Normal = 0,
        };

        public enum ErrorTypeEnum
        {
            FillInNiceName = 10000,
            NullRoomID = 10001,
            CancelReady = 10002,
            ObtainRandomHero = 10003,
            NeedLockTarget = 10004,
            ManagerChange = 10010,
            AbsentSkillNULL = 10011,
            NeedAItermID = 10012,
            NeedABagID = 10013,
            BagIsEmpty = 10014,
            WarnningToSelectHero = 10015,
            GoodsIsCoolingDown = 10016,
            GoodsNULLUseTimes = 10017,
            TalkSendNull = -16,
        }

        public enum ResultEnum
        {
            Normal,
            DelEntityFailed,
        }
    }
}
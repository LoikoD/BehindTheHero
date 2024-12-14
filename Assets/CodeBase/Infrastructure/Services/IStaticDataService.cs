using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadMonsters();
        void LoadLevels();
        void LoadDialogues();
        EnemyStaticData ForMonster(EnemyTypeID typeID);
        KnightStaticData ForKnight();
        PlayerStaticData ForHero();
        LevelStaticData ForLevel(string sceneKey);
        DialogueStaticData ForDialogue(string sceneKey);
        SceneListStaticData AllScenes();
    }
}
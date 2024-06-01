namespace Project
{
    public class FilePath
    {
        public const string SPRITES_PATH = "Sprites/";
        public const string UI_SPRITES_PATH = SPRITES_PATH + "UI/";

        // Sprites
        public const string TILE_MAP_SPRITE_PATH = SPRITES_PATH + "tilemap";
        public const string PLAYER_SPRITE_PATH = SPRITES_PATH + "player";
        public const string SMASH_BUTTON_SPRITE_PATH = UI_SPRITES_PATH + "smash_button";
        public const string GAME_LOGO_SPRITE_PATH = UI_SPRITES_PATH + "title_logo";

        // Atlas
        public const string TILE_MAP_ATLAS_PATH = SPRITES_PATH + "atlas";
        public const string PLAYER_ATLAS_PATH = SPRITES_PATH + "player_animation";
        public const string SMASH_BUTTON_ATLAS_PATH = UI_SPRITES_PATH + "smash_button_animation";

        // Fonts
        public const string FONT_PATH = "Font";

        //FMOD
        public const string FMOD_BANK_PATH = "Content/Desktop/Master.bank";

        // CutScenes
        public const string CUTSCENE_1_PATH = @"Content/Dialogues/cutscene_1.dn";
    }
}

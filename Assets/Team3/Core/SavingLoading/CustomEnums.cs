namespace Team3.SavingLoading
{
    public enum AudioChannel
    {
        Master = 0,
        Music = 1,
        SFX = 2,
        Ambient = 3,
        UI = 4
    }

    public enum DropDownValue
    {
        DisplayMode = 0,
        ResolutionMode = 1,
        FpsMode = 2
    }

    public enum KeyBoardMousePlayerAction
    {
        Walk = 0,
        StriveLeft = 1,
        WalkBack = 2,
        StriveRight = 3,
        Sprint = 4,
        Jump = 5,
        Shoot = 6,
        Reload = 10
    }

    public enum GamePadPlayerAction
    {
        Sprint = 2,
        Jump = 3,
        Shoot = 4,
        Reload = 8
    }
}

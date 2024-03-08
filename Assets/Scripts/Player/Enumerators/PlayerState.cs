
public enum PlayerState
{
    None,       //Base state, no additional effects are applied.
    attacking,  //Induced when attacking. Just allows us to make sure we don't start another attack when already attacking. Might need to delete this.
    dashing,    //Used when the player is dashing, do not set X velocity after dashing.
    launched,   //Induced when launched. This just lets us know to stop the old launch coroutine and start a new one. Disables some physics.
    helpless,   //Induced after running out of jumps while in the air. Sometimes called "Freefall" https://www.ssbwiki.com/Helpless
    intangible, //Induced by dodging. Cannot be hit or pushed by other players. https://www.ssbwiki.com/Intangibility
    shielding,  //Induced by shielding, Cannot be damaged but doing any other action will exit this state.
    grabbing,   //Induced by grabbing another character.
    grabbed,    //Induced when being grabbed.
}
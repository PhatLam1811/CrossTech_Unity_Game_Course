using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StartDialog : BaseDialog
{
    public const string ANIMATOR_SHOW = "Fade";
    public const string ANIMATOR_HIDE = "ScaleSmall";

    public override void ClickCloseDialog()
    {
        base.ClickCloseDialog();

        this.StartGame();
    }

    public override void OnHide()
    {
        this._animator.Play(ANIMATOR_HIDE);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}

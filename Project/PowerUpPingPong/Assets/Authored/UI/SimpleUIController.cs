using System.Collections;
using UnityEngine;

public enum AnimTriggers
{
    hideMenu,
    showMenu,
    hidePause,
    showPause,
}

public class SimpleUIController : MonoBehaviour
{
    [SerializeField] private Animator _canvasAnim;
    [SerializeField] private AnimTriggers _trigger;

    private IEnumerator Start()
    {
        if (_canvasAnim != null) 
        {
            HidePause();
            yield return new WaitForSeconds(1f);
            //ShowMenu();
        }
    }

    public void ShowMenu()
    {
        if (_canvasAnim)
        {
            _trigger = AnimTriggers.showMenu;
            _canvasAnim.SetBool(AnimTriggers.hideMenu.ToString(), false);
            _canvasAnim.SetBool(_trigger.ToString(), true);
        }
    }

    public void HideMenu()
    {
        if (_canvasAnim)
        {
            _trigger = AnimTriggers.hideMenu;
            _canvasAnim.SetBool(AnimTriggers.showMenu.ToString(), false);
            _canvasAnim.SetBool(_trigger.ToString(), true);
        }
    }

    public void ShowPause() 
    {
        if (_canvasAnim)
        {
            _trigger = AnimTriggers.showPause;
            _canvasAnim.SetBool(AnimTriggers.hidePause.ToString(), false);
            _canvasAnim.SetBool(_trigger.ToString(), true);
        }
    }

    public void HidePause()
    {
        if (_canvasAnim)
        {
            _trigger = AnimTriggers.hidePause;
            _canvasAnim.SetBool(AnimTriggers.showPause.ToString(), false);
            _canvasAnim.SetBool(_trigger.ToString(), true);
        }
    }
}

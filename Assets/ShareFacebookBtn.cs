using UnityEngine;
using System.Collections;

public class ShareFacebookBtn : Button {

	private const string FACEBOOK_URL = "https://www.facebook.com/sharer/sharer.php";
	
	public override void OnButtonClick() {
		Application.OpenURL ( FACEBOOK_URL + "?u=" + GameConsts.AppLinkAndroid );
	}

}

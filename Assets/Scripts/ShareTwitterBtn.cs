using UnityEngine;
using System.Collections;

public class ShareTwitterBtn : Button {
	
	private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
	
	public override void OnButtonClick() {
		
		Application.OpenURL( TWITTER_ADDRESS + "?text=" + WWW.EscapeURL( GameConsts.ShareMessage ) );
	}

}

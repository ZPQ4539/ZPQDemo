package com.example.u2a;

import androidx.appcompat.app.AppCompatActivity;
import android.app.Activity;
import android.os.Bundle;
import android.widget.Toast;
import com.unity3d.player.UnityPlayer;

public class MainActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    //unity调用Android
    public void UnityCallAndroid () {

        Toast.makeText(this,"unity调用android成功", Toast.LENGTH_LONG).show();

        AndroidCallUnity();
    }

    private void AndroidCallUnity() {
        //第1个参数为Unity场景中用于接收android消息的对象名称
        //第2个参数为对象上的脚本的一个成员方法名称（脚本名称不限制）
        //第3个参数为unity方法的参数
        UnityPlayer.UnitySendMessage("receiveObj", "UnityMethod", "This is args.");
    }
}
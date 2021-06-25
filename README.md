# Unity設定
## 参照
- （無印）https://developer.oculus.com/documentation/unity/unity-conf-settings/
- ○: フレームシンセシスを参照
https://framesynthesis.jp/tech/unity/oculusquest/
- ▲: Oculus blogにのみ記載
https://developer.oculus.com/blog/tech-note-unity-settings-for-mobile-vr/?locale=cs_CZ

## その他記号
- (): デフォルトで変更必要なし
- ■: 適宜変更、個人に合わせて変更
- ?: 設定場所が見つからない

## BuildSettings
- BuildSettings > Texture Comppression => ATSCに変更
- ■ 上記 > Run Device => Oculus Quest2に変更
- Androidに切り替えてSwitch Platformをクリック

## ■ 製品の詳細情報
- （ Player > Company Name => 未設定（DefaultCompany））
- （上記 > Product Name => magic_battles）
-  上記 > Version => 0.1 適宜変更

## パッケージ識別詳細情報
- （Player > Identification > Package Name => 変更なし、デフォルトで記入済み）
- ■ 上記 > Version => 0.1
- ■ 上記 > Bundle Version Code => 1 適宜変更
- 上記 > Minimum API Level => Android 6.0 Marshmallowに変更
- （上記 > Target API Level => Automatic (highest installed)

## XR Plugin Management関連
- PlayerSettings > XR Plugin Management => Install XR Plugin Managementをクリック
- 上記（XR Plugin Management） => Oculusを選択
- ○ XR Plugin Management > Oculus > Stereo Rendering Mode => Multiviewに変更
https://developer.oculus.com/documentation/unity/unity-single-pass/

## レンダリング設定
- ProjectSettings > Player > Other Settings > Rendering > Color Space => Linearに変更
- （上記 > Auto Graphics API => 選択を外す）
- （上記 > Multithreaded Rendering => 選択する）
- ? Low Overhead Mode => 選択する
- ○ 上記 > Dynamic Batching => 選択する
- ○ （上記 > Static Batching => 選択する）
- ○ （上記 > Graphics Jobs => 選択しない）

## 品質設定
- ProjectSettings > Quality > Pixel Light Count => 1に設定
- 上記 > Anisotropic Textures => Per Textureに変更
- 上記 > Anti Aliasing => 4x Multi Samplingに変更
- 上記 > Soft Particles => 選択を外す
- （上記 > Realtime Reflections Probes => 選択する）
- （上記 > Billboards Face Camera => 選択する）

## ■ その他後々に見るかもしれない設定
- ○ Player > Other Settings > Configuration > Scripting Backend => 開発中はMono, リリース時はIL2CPP。
https://framesynthesis.jp/tech/unity/oculusquest/#%E3%83%93%E3%83%AB%E3%83%89%E8%A8%AD%E5%AE%9A
- ▲ Player > Other Settings > Optimaization > Prebake Collision Meshes => もしapkファイルの大きさが問題でなければ選択.（一旦選択無しで良さそう）
- ▲ 上記 > Keep Loaded Shaders Alive => If compiling shaders on demand is an issue, check this box.
- ▲ techblogのaudio周り、Quality周り、Graphics周り

# その他
- Assets/Oculus/SampleFramework/Usage/AppDeeplink/AppDeeplinkUI.cs
ファイルは、コンパイル時に以下のエラーが発生するため全文コメントアウト中
AppDeeplinkUI.cs(37,18): error CS0103: The name 'Oculus' does not exist in the current context
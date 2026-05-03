using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FavoritesManager
{
    public partial class MainWindow : Window
    {
        private List<ResourceItem> resources = new List<ResourceItem>();
        private List<ResourceItem> filteredResources = new List<ResourceItem>();
        private string browserSettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "browser.txt");

        public MainWindow()
        {
            InitializeComponent();
            InitializeCategoryComboBox();
            LoadResources();
            UpdateDisplay();
        }

        private void InitializeCategoryComboBox()
        {
            cmbCategory.Items.Add(new ComboBoxItem { Content = "全部资源", Tag = "All" });
            cmbCategory.Items.Add(new ComboBoxItem { Content = "免费资源", Tag = "Free" });
            cmbCategory.Items.Add(new ComboBoxItem { Content = "付费资源", Tag = "Paid" });
            cmbCategory.SelectedIndex = 0;
        }

        private void AddDefaultResource(string name, string url, string category, string type, string tooltip)
        {
            resources.Add(new ResourceItem
            {
                Id = Guid.NewGuid(),
                Name = name,
                Url = url,
                Description = tooltip,
                Category = category,
                Type = type,
                CreatedDate = DateTime.Now,
                LastAccessed = DateTime.MinValue,
                IsDefault = true
            });
        }

        private void LoadResources()
        {
            AddDefaultResource("GitHub", "https://github.com", "代码托管平台", "Free", "目前全球最大、最流行的代码托管平台。");
            AddDefaultResource("Gitee", "https://gitee.com", "代码托管平台", "Free", "码云,由OSCHINA开源中国社区推出的国产平台，是中国最大的代码托管平台。");
            AddDefaultResource("nuget", "https://www.nuget.org", "其他", "Free", ".NET平台的包管理器官方网站，.NET开发常常需要用安装nuget包，在这可以尽情搜索");
            AddDefaultResource("componentaceACE", "https://www.componentace.com", "其他", "Free", "一家专注于软件开发组件的公司，主要为开发者提供数据库、压缩、加密等领域的功能组件，支持 Delphi、C++ Builder、.NET等多种开发平台和框架。");
            AddDefaultResource("MSDN", "https://msdn.itellyou.cn", "其他", "Free", "微软官方Windows系统镜像下载地址");
            AddDefaultResource("NEXT, ITELLYOU", "https://next.itellyou.cn/Original", "其他", "Free", "第三方Windows系统镜像下载地址");
            AddDefaultResource("音乐解锁", "http://unlock.music.hi.cn", "其他", "Free", "移除音乐的加密保护,目前支持网易云音乐(ncm),QQ音乐(qmc,mflac,mgg),酷狗音乐(kgm),虾米音乐(xm),酷我音乐(.kwm)");
            AddDefaultResource("风车漫画", "https://www.fengchemh.com", "漫画", "Free", "国产、日版、韩国、欧美漫画网站");
            AddDefaultResource("漫画站", "https://www.manhuazhan.org", "漫画", "Free", "国产、日版、韩国、欧美漫画网站");
            AddDefaultResource("动漫屋", "https://www.dm5.com", "漫画", "Free", "国产和日版的漫画网站");
            AddDefaultResource("漫漫漫画", "https://www.manmanapp.com", "漫画", "Free", "漫漫漫画是一个专注于提供正版原创漫画内容的平台，致力于将二次元宅腐暖萌一网打尽，成为国内正版漫画的集结地");
            AddDefaultResource("漫本", "https://www.manben.com", "漫画", "Free", "漫本拥有上万部各类题材的漫画作品，包括恋爱漫画、热血漫画、武侠漫画、冒险漫画、科幻漫画、同人漫画、治愈漫画、内涵漫画、都市漫画、悬疑漫画、校园漫画、总裁漫画、搞笑漫画、玄幻漫画等多种类型");
            AddDefaultResource("动漫嗨", "https://www.dongmanhi.com", "漫画", "Free", "拥有热血、恋爱、都市、纯爱、穿越、生活、脑洞、侦探、节操等风格漫画");
            AddDefaultResource("推次元", "https://a2cy.com", "Cosplay", "Free", "动漫、游戏、影视等二次元角色cosplay");
            AddDefaultResource("COS二次元-Cosplay社区", "https://www.cos2cy.com", "Cosplay", "Free", "动漫、游戏、影视等二次元角色cosplay");
            AddDefaultResource("次元岛", "https://www.cosplay8.com/index.html", "Cosplay", "Free", "动漫、游戏、影视等二次元角色cosplay");
            AddDefaultResource("Cosz", "https://cosz.com", "Cosplay", "Free", "动漫、游戏、影视等二次元角色cosplay");
            AddDefaultResource("cosplay啦", "https://www.cosplayla.com", "Cosplay", "Free", "动漫、游戏、影视等二次元角色cosplay");
            AddDefaultResource("strategywiki", "https://strategywiki.org/wiki/Main_Page", "维基百科", "Free", "一个各种主机掌机游戏的维基百科网站");
            AddDefaultResource("multimedia", "https://wiki.multimedia.cx/index.php/", "维基百科", "Free", "一个科普音频、视频、图片、游戏格式的维基百科网站");
            AddDefaultResource("pixabay", "https://pixabay.com/zh/illustrations", "素材网站", "Free", "免费正版高清插画素材库");
            AddDefaultResource("CG模型网", "https://www.cgmodel.com", "素材网站", "Paid", "3d模型库");
            AddDefaultResource("国漫图库", "https://guomantuku.com", "素材网站", "Free", "AI国漫女神图库");
            AddDefaultResource("爱小舞国漫壁纸社", "https://www.52gmbz.cn", "素材网站", "Paid", "AI国漫女神图库，看看免费的即可，不需要花钱开VIP");   
            AddDefaultResource("小土豆国漫壁纸", "https://www.guomanbizhi.com", "素材网站", "Paid", "AI国漫女神图库，该网站为付费资源，看看就行，付费下载就算了");
            AddDefaultResource("爱给网", "https://www.aigei.com/design", "素材网站", "Free", "一个包含在线设计、模板、AI绘图、图库、元素、背景、图标、矢量、字体、UI、服装、PPT的多用途网站");
            AddDefaultResource("触站", "https://www.huashi6.com", "素材网站", "Free", "原创画师分享平台，似乎是搬运P站画师作品的网站");
            AddDefaultResource("佩可爱动漫", "https://acg.pekolove.net", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("樱花动漫", "https://www.sci-star.com", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("樱之空动漫", "https://skr.skrcc.cc:666/?ref=www.zhaicangku.com", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("风车动漫", "https://fche.cc", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("咪咕番", "https://www.gugu3.com", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("Omofun动漫", "https://omofun.in", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("MuteFun动漫", "https://www.mutean.com", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("girigiri爱动漫", "https://bgm.girigirilove.com", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("咕咕番", "https://www.gugu3.com", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("E站弹幕网", "https://www.ezdmw.site", "动漫", "Free", "一个免费的可以在线播放动漫的网站，并且可以使用视频下方的迅雷链接下载到本地，非常良心");
            AddDefaultResource("漫猫动漫", "https://www.comicat.org", "动漫", "Free", "一个免费的可以下载动漫资源的网站");
            AddDefaultResource("次元狗动漫", "https://www.acgndog.com", "动漫", "Free", "一个提供大量的动漫、漫画、游戏和轻小说资源的网站，能免费看动漫就算了居然还能免费下载游戏，绝对是游戏和动漫爱好者的必选网站");
            AddDefaultResource("柒番动漫", "https://www.qifun.cc", "动漫", "Free", "一个免费的可以在线播放动漫的网站");        
            AddDefaultResource("番薯动漫", "https://www.fsdm02.com", "动漫", "Free", "一个免费的可以在线播放动漫的网站");
            AddDefaultResource("TZ素材网", "https://www.tzsucai.com/soft.html", "开发工具", "Free", "TZ素材网，拥有三维设计、编程开发、动画设计、平面设计、渲染器、办公软件、视频制作、软件汉化等...");
            AddDefaultResource("编程师", "https://123.w3cschool.cn/webtools", "开发工具", "Free", "拥有在线IDE工具、站长辅助工具、编码转换工具、CSS在线工具、正则表达式工具、颜色工具、密码工具、XML在线工具、格式化美化工具、编程工具");
            AddDefaultResource("视觉小说游戏信息数据库", "https://vndb.org", "其他", "Free", "一个galgame游戏资源数据库");
            AddDefaultResource("CnGal资料站", "https://www.cngal.org", "其他", "Free", "一个galgame游戏百科资料站");
            AddDefaultResource("Galgame月谣", "https://www.sayafx.vip", "游戏下载", "Free", "一个免费GalGame游戏资源下载网站");
            AddDefaultResource("SteamGalgame", "https://steamgalgame.com", "其他", "Free", "一个收集Steam上中文Galgame的网站，本网站会显示galgame游戏的价格和折扣，方便大家快速查找和获取Steam上最新的官方中文Galgame");
            AddDefaultResource("xxacg", "https://xxacg.net/", "游戏下载", "Free", "一个免费的galgame游戏资源下载网站，注册账号后就可以免费下载，只是该网站相应较慢，需要耐心等待");
            AddDefaultResource("真红小站", "https://www.shinnku.com", "游戏下载", "Free", "一个免费GalGame游戏资源下载网站，不需要注册账号即可下载");
            AddDefaultResource("MyGalgame", "https://www.ttloli.com", "其他", "Free", "一个曾经的galgame游戏资源分享网站，现在似乎只能看下幻灯片了");
            AddDefaultResource("游戏工厂", "https://gamefabrique.com", "游戏下载", "Free", "一个外国人的游戏资源免费下载网站，访问速度很慢");
            AddDefaultResource("All My Roms", "https://www.allmyroms.net", "游戏下载", "Free", "一个外国人的掌机游戏免费资源下载网站");
            AddDefaultResource("老男人游戏网", "https://www.oldmantvg.net", "游戏下载", "Paid", "一个主机掌机游戏资源下载网站，拥有索尼、微软、任天堂和世嘉的部分机型游戏rom，不包括ps4和switch，30TB的游戏，2024年花50元开会员支持了一下");
            AddDefaultResource("跑跑车游戏网", "https://www.paopaoche.net/tv/101581.html", "游戏下载", "Free", "一个主机、掌机、PC游戏资源下载网站");
            AddDefaultResource("K73资源网", "http://www.k73.com/down", "游戏下载", "Free", "一个主机、掌机、PC游戏资源下载网站");
            AddDefaultResource("散月的星空", "https://www.sygame515.com", "游戏下载", "Free", "一个Switch和PS4游戏免费资源下载网站");
            AddDefaultResource("奥德彪计划", "https://2468c.com", "游戏下载", "Free", "一个PS4和PS5游戏免费资源下载网站");
            AddDefaultResource("GameFreer资源网", "https://www.gamefreer.com", "游戏下载", "Free", "一个端游手游和任天堂游戏免费资源下载网站，该网站为防止网络爬虫倒卖资源而对链接进行了加密，可通过手机浏览器获取链接再用电脑下载");
            AddDefaultResource("梨子乐游戏", "https://lzlgo.com", "游戏下载", "Free", "一个PC游戏免费资源下载网站");
            AddDefaultResource("资源避难所", "https://www.flysheep6.com", "游戏下载", "Free", "一个PC游戏免费资源下载网站");
            AddDefaultResource("牛游戏网", "https://www.newyx.net/zt/hj", "游戏下载", "Free", "一个PC游戏免费资源下载网站，该网站不稳定，有概率重定向导致无法打开");
            AddDefaultResource("游戏大桶", "https://www.gamekeg.com", "游戏下载", "Paid", "一个Switch、PS4、PS5和PC游戏资源下载网站，升级会员会跳转到switch520，90块钱永久会员，每日可无限下载");
            AddDefaultResource("GAME520", "https://www.gamer520.com/zhangji", "游戏下载", "Free", "一个Switch、PS4和PC游戏免费资源下载网站");
            AddDefaultResource("沙克游戏", "https://shaqzone.com", "游戏下载", "Free", "一个Switch和PC游戏免费资源下载网站，夸克网盘链接，不喜欢夸克网盘的绕过吧");
            AddDefaultResource("Koyso", "https://koyso.to", "游戏下载", "Free", "一个PC游戏免费资源下载网站");         
            AddDefaultResource("小叽资源", "https://steamzg.com", "游戏下载", "Free", "一个PC游戏免费资源下载网站");
            AddDefaultResource("romsgames", "https://www.romsgames.net/roms", "游戏下载", "Free", "一个主机掌机游戏免费资源下载网站");
            AddDefaultResource("romhustler", "https://romhustler.org", "游戏下载", "Free", "一个主机掌机游戏免费资源下载网站");
            AddDefaultResource("retrostic", "https://www.retrostic.com", "游戏下载", "Free", "一个主机掌机游戏免费资源下载网站");
            AddDefaultResource("consoleroms", "https://www.consoleroms.com", "游戏下载", "Free", "一个主机掌机游戏免费资源下载网站");
            AddDefaultResource("oldemu", "https://www.oldemu.com", "游戏下载", "Free", "一个主机掌机游戏免费资源下载网站");
            AddDefaultResource("romulation", "https://www.romulation.org", "游戏下载", "Free", "一个主机掌机游戏免费资源下载网站");
            AddDefaultResource("romspedia", "https://www.romspedia.com", "游戏下载", "Free", "一个主机掌机游戏免费资源下载网站");
            AddDefaultResource("烧录卡", "https://shaoluka.com", "游戏下载", "Paid", "一个任天堂游戏资源下载网站，游戏分类不明确，看不出游戏的具体平台，59.9元永久VIP，全站资源免费下载，但是没注明每天可下载多少次，差评");
            AddDefaultResource("咸鱼单机", "https://www.xianyudanji.to", "游戏下载", "Paid", "一个Switch和PC游戏资源下载网站，永久VIP原价129，限时特价49，每日可下载50次，就是不知道它这个限时特价的时间范围，如果是长期的话，那它的性价比还是较高的");
            AddDefaultResource("SKY电玩人", "https://sky.sfcrom.com", "游戏下载", "Paid", "一个Switch和PC游戏资源下载网站，永久VIP最低25，每天可下载30次，39.9无下载次数限制");
            AddDefaultResource("Game Share", "https://www.gameshare.cc", "游戏下载", "Free", "一个PC游戏免费资源下载网站，完全免费，不需要注册账号");
            AddDefaultResource("Ms游戏库", "https://www.msgamego.com", "游戏下载", "Paid", "一个Switch和PC游戏资源下载网站，永久VIP98，每天最多只能下载10次");
            AddDefaultResource("GBT单机游戏空间", "https://gbtgame.org", "游戏下载", "Paid", "一个主机、掌机和PC游戏资源下载网站，永久VIP49.8,全站无限制下载，不限制每天的下载次数");
            AddDefaultResource("GAME中文网", "https://www.ns211.com", "游戏下载", "Paid", "一个Switch和PC游戏资源下载网站，80元终身VIP，付费资源免费下载，资源终身无限下载");
            AddDefaultResource("nspdown", "https://www.nspdown.com", "游戏下载", "Paid", "一个Switch游戏资源下载网站，60元终身VIP，每天可下载50个资源，全部资源免费下载");
            AddDefaultResource("nsboy", "https://www.nsboy.net/portal.php", "游戏下载", "Paid", "一个Switch游戏资源下载网站，89元终身VIP，全部资源免费下载，但是没写每天可下载多少次");
            AddDefaultResource("橘子下载", "https://www.juzixiazai.com", "游戏下载", "Free", "一个Switch游戏资源下载网站，全部资源免费下载，连账号都不用注册就可以白嫖");
            AddDefaultResource("nekogal", "https://www.nekogal.com", "游戏下载", "Free", "一个Galgame游戏资源免费下载网站，LSP的天堂");
            AddDefaultResource("TouchGal", "https://www.touchgal.us", "游戏下载", "Free", "一个免费,高质量的Galgame资源下载站");
            AddDefaultResource("18rwan", "https://www.18rwan.com", "游戏下载", "Paid", "一个PC单机游戏资源下载网站，198元终身VIP，全部VIP资源免费，全部VIP内容可查看，每天可下载30次");
            AddDefaultResource("游戏星辰", "https://www.2023game.com", "游戏下载", "Paid", "一个各种主机掌机PC游戏资源下载网站，90元终身VIP，但是没写每天可下载多少次资源，充值界面那几个价位段连充值服务描述都没有，真TM坑爹");
            AddDefaultResource("Live2DHub", "https://live2dhub.com", "论坛社区", "Free", "一个端游和手游资源社区，主要交流游戏解包、Spine和Live2D、mod制作");
            AddDefaultResource("psx-place", "https://www.psx-place.com", "论坛社区", "Free", "一个专注于PlayStation相关改装、开发和自制软件的知名社区论坛");
            AddDefaultResource("reshax", "https://reshax.com", "论坛社区", "Free", "一个专注于游戏模组、游戏解包的社区论坛");
            AddDefaultResource("Nexus Mods", "https://www.nexusmods.com", "游戏模组", "Free", "全球最大的游戏模组平台，涵盖几乎所有主流游戏");
            AddDefaultResource("Mod DB", "https://www.moddb.com", "游戏模组", "Free", "老牌模组网站，包含大量独立游戏模组和自制游戏");
            AddDefaultResource("saintsrowmods", "https://www.saintsrowmods.com/forum", "论坛社区", "Free", "一个分享各种游戏解包工具和mod交流的论坛社区");
            AddDefaultResource("pastebin", "https://pastebin.com/", "代码托管平台", "Free", "一个专门分享文本、代码的网站，里面也有游戏解包用的代码");
            AddDefaultResource("SCS Software", "https://forum.scssoft.com/", "轮眼社区", "Free", "一个交流各种游戏解包和mod的论坛社区");
            AddDefaultResource("allmods.net", "https://allmods.net", "游戏模组", "Free", "一个免费的模组下载网站");
            AddDefaultResource("ragezone", "https://forum.ragezone.com", "论坛社区", "Free", "一个分享各种游戏解包工具和mod交流的论坛社区");
            AddDefaultResource("fuwanovel", "https://forums.fuwanovel.moe", "论坛社区", "Free", "一个交流各种游戏解包和分享解包工具的论坛社区");
            AddDefaultResource("GBATemp", "https://gbatemp.net", "论坛社区", "Free", "一个分享各种游戏解包工具和mod交流的论坛社区");
            AddDefaultResource("GameBanana(香蕉网)", "https://gamebanana.com", "游戏模组", "Free", "专注于CS、Garry's Mod等Source引擎游戏的模组");
            AddDefaultResource("ZenHAX", "https://zenhax.com", "论坛社区", "Free", "一个专注于游戏模组、游戏解包的社区论坛,该论坛已关闭，没有新帖，只能去里面翻找一些历史旧帖");
            AddDefaultResource("modworkshop", "https://modworkshop.net", "游戏模组", "Free", "一个创造和下载各种游戏模组的平台");
            AddDefaultResource("cs.rin.ru", "https://cs.rin.ru/forum/viewtopic.php?t=100672", "论坛社区", "Free", "一个虚幻引擎游戏交流论坛，主要更新各种虚幻引擎AES密钥、bms脚本、AES finder和讨论解包方法");
            AddDefaultResource("Gildor's Forums", "https://www.gildor.org/smf/index.php?board=37.0", "论坛社区", "Free", "一个虚幻引擎游戏交流论坛，主要讨论各种虚幻3/4/5游戏解包方法");
            AddDefaultResource("Falcom-模组制作工具", "http://modding.tistory.com/3", "游戏模组", "Free", "Falcom游戏工具");
            AddDefaultResource("RomHacking", "https://www.romhacking.net/forum/index.php?board=5.0", "论坛社区", "Free", "一个外国人的游戏论坛，主要讨论游戏解包提取");
            AddDefaultResource("PlayStation专用固件下载", "https://archive.midnightchannel.net/SonyPS", "其他", "Free", "一个可以下载psv、psp、ps3、ps4和ps5官方固件的网站，其中ps4和ps5的固件仍在更新");
            AddDefaultResource("darthsternie", "https://darthsternie.net", "其他", "Free", "一个可以下载xbox自制软件、3ds、switch、psp、psv、ps2、ps3、ps4和ps5固件的网站，看着挺全面，但是部分机型固件好像更新的不是那么及时");
            AddDefaultResource("switchfirm", "https://switchfirm.org", "其他", "Free", "一个专门下载switch固件的网站");
            AddDefaultResource("foobar2000组件官方下载网站", "https://www.foobar2000.org/components", "其他", "Free", "收录了大约200多个foobar2000组件，不过现在使用64位系统的用户无法安装那些仅支持32位系统的组件");
            AddDefaultResource("quickbms脚本官方下载网站", "https://aluigi.altervista.org", "其他", "Free", "收录了超过2000多个bms脚本，不过很多bms脚本已经失效或者根本无效，此官网早已经停止更新，因此有更多的bms脚本未收录，至于quickbms主程序可以找我下载汉化版的");
            AddDefaultResource("123apps", "https://123apps.com/cn", "其他", "Free", "一个用于影片、音频、PDF与档案转换的线上工具，神奇之处在于它是使用浏览器来在线处理，不需要下载各种软件");
            AddDefaultResource("Garbro游戏档案类型", "https://morkt.github.io/GARbro/supported.html", "其他", "Free", "汇总了Garbro支持的游戏档案和测试过的游戏列表");
            AddDefaultResource("Indienova", "https://indienova.com", "论坛社区", "Free", "一个专注于独立游戏的中文社区与媒体平台，在这你可以浏览游戏在steam和主机掌机平台的售价信息，可以发布自己的游戏，可以查看开发者写的文章，甚至可以试玩一些游戏");
            AddDefaultResource("segaxtreme", "https://segaxtreme.net", "论坛社区", "Free", "一个专门讨论掌机和单机游戏的论坛社区，没看到主机游戏，光看网址的话以为仅仅是个sega论坛");
            AddDefaultResource("dllme", "https://www.dllme.com", "其他", "Free", "一个神奇网站，里面收录了大量的dll，比如oo2core_9_win64.dll这种经常出现在游戏或者解包工具的dll在这里已经不足为奇了，之所以说它神奇是从解包者的角度考虑，如果你为了解包某个游戏缺一个重要的dll的话，可以在这里找找看，完善你的解包工具");
            AddDefaultResource("rarewares", "https://www.rarewares.org/rrw/programs.php", "其他", "Free", "音频视频编解码器");
            AddDefaultResource("rarewares", "https://www.rarewares.org/index.php", "其他", "Free", "音频视频编解码器");
            AddDefaultResource("CODECS", "https://www.free-codecs.com", "其他", "Free", "音频视频编解码器");
            AddDefaultResource("emuparadise", "https://www.emuparadise.me", "其他", "Free", "一个复古游戏镜像、模拟器等资源网站");
            AddDefaultResource("kuangyx", "https://kuangyx.cn", "其他", "Free", "一个一个前端开发者的个人技术博客+组件库+工具站");
            AddDefaultResource("psx-spx", "https://psx-spx.consoledev.net", "其他", "Free", "一个PlayStation的wiki百科网站");

            filteredResources = resources.ToList();
            SortResourcesByName();
        }
        private void SortResourcesByName()
        {
            resources = resources.OrderBy(r => r.Name).ToList();
            filteredResources = filteredResources.OrderBy(r => r.Name).ToList();
        }
        private void UpdateDisplay()
        {
            lstResources.ItemsSource = null;
            lstResources.ItemsSource = filteredResources;

            var totalCount = resources.Count;
            var filteredCount = filteredResources.Count;
            var defaultCount = resources.Count(r => r.IsDefault);
            var customCount = resources.Count(r => !r.IsDefault);

            txtCount.Text = $"共{filteredCount}/{totalCount}个资源(默认:{defaultCount}自定义:{customCount})";
        }

        private void FilterResources()
        {
            var category = (cmbCategory.SelectedItem as ComboBoxItem)?.Tag?.ToString();
            var searchText = txtSearch.Text.ToLower();

            filteredResources = resources.Where(r =>
                (category == "All" || category == null || r.Type == category) &&
                (string.IsNullOrEmpty(searchText) ||
                 r.Name.ToLower().Contains(searchText) ||
                 r.Url.ToLower().Contains(searchText) ||
                 r.Description.ToLower().Contains(searchText) ||
                 r.Category.ToLower().Contains(searchText))
            ).OrderBy(r => r.Name).ToList();

            UpdateDisplay();
        }

        private void lstResources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = lstResources.SelectedItem as ResourceItem;
            if (selected != null)
            {
                txtDetailName.Text = selected.Name;
                txtDetailUrl.Text = selected.Url;
                txtDetailCategory.Text = selected.CategoryDisplay;
                txtDetailType.Text = selected.TypeDisplay;

                if (selected.IsDefault)
                {
                    txtStatus.Text = $"🔒{selected.Name}是默认资源，受保护不可删除";
                }
                else
                {
                    txtStatus.Text = $"已选择:{selected.Name}";
                }

                btnOpen.IsEnabled = true;
                btnEdit.IsEnabled = !selected.IsDefault;
                btnDelete.IsEnabled = true;
            }
            else
            {
                txtDetailName.Text = "-";
                txtDetailUrl.Text = "-";
                txtDetailCategory.Text = "-";
                txtDetailType.Text = "-";
                txtStatus.Text = "就绪";
                btnOpen.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }
        private string? GetSavedBrowserPath()
        {
            try
            {
                if (File.Exists(browserSettingsFile))
                {
                    return File.ReadAllText(browserSettingsFile).Trim();
                }
            }
            catch { }
            return null;
        }

        private void SaveBrowserPath(string browserPath)
        {
            try
            {
                File.WriteAllText(browserSettingsFile, browserPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存浏览器设置失败:{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OpenUrlWithBrowser(string url, string resourceName)
        {
            try
            {
                string? browserPath = GetSavedBrowserPath();

                if (!string.IsNullOrEmpty(browserPath) && File.Exists(browserPath))
                {
                    Process.Start(new ProcessStartInfo(browserPath, url));
                    txtStatus.Text = $"已使用自定义浏览器打开:{resourceName}";
                }
                else
                {
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    txtStatus.Text = $"已使用系统默认浏览器打开: {resourceName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开网址失败:{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void txtDetailUrl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selected = lstResources.SelectedItem as ResourceItem;
            if (selected != null && !string.IsNullOrEmpty(selected.Url))
            {
                OpenUrlWithBrowser(selected.Url, selected.Name);
            }
        }
        private void SelectAndSaveBrowser()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Title = "选择默认浏览器";
            dialog.Filter = "浏览器程序|*.exe";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            if (dialog.ShowDialog() == true)
            {
                SaveBrowserPath(dialog.FileName);
                MessageBox.Show($"已设置默认浏览器为:{Path.GetFileNameWithoutExtension(dialog.FileName)}", "设置成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var selected = lstResources.SelectedItem as ResourceItem;
            if (selected != null)
            {
                selected.LastAccessed = DateTime.Now;
                string? browserPath = GetSavedBrowserPath();
                if (string.IsNullOrEmpty(browserPath) || !File.Exists(browserPath))
                {
                    var result = MessageBox.Show("是否要选择默认浏览器?\n点击\"是\"选择浏览器，\"否\"使用系统默认浏览器。", "选择浏览器", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        SelectAndSaveBrowser();
                    }
                }
                OpenUrlWithBrowser(selected.Url, selected.Name);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ResourceEditDialog();
            if (dialog.ShowDialog() == true)
            {
                var newItem = dialog.ResourceItem;
                newItem.IsDefault = false;
                newItem.Id = Guid.NewGuid();
                newItem.CreatedDate = DateTime.Now;
                newItem.LastAccessed = DateTime.MinValue;

                resources.Add(newItem);
                SortResourcesByName();
                FilterResources();
                txtStatus.Text = "资源添加成功";
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = lstResources.SelectedItem as ResourceItem;
            if (selected != null)
            {
                if (selected.IsDefault)
                {
                    MessageBox.Show($"默认链接'{selected.Name}'受保护，无法编辑。", "编辑保护", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dialog = new ResourceEditDialog(selected);
                if (dialog.ShowDialog() == true)
                {
                    var index = resources.IndexOf(selected);
                    if (index != -1)
                    {
                        resources[index] = dialog.ResourceItem;
                        SortResourcesByName();
                        FilterResources();
                        txtStatus.Text = "资源修改成功";
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择一个资源", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = lstResources.SelectedItem as ResourceItem;
            if (selected != null)
            {
                if (selected.IsDefault)
                {
                    MessageBox.Show($"你小子想删除'{selected.Name}'这个链接？不想混了吗？这是默认链接，无法删除！", "删除保护", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show($"确定要删除资源'{selected.Name}' 吗？", "确认删除",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    resources.Remove(selected);
                    SortResourcesByName();
                    FilterResources();
                    txtStatus.Text = "资源删除成功";
                }
            }
            else
            {
                MessageBox.Show("请先选择一个资源", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            var aboutDialog = new AboutDialog();
            aboutDialog.Owner = this;
            aboutDialog.ShowDialog();
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterResources();
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterResources();
        }
    }
}

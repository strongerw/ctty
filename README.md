# ConsoleTTY

#### 介绍
小巧简洁的命令行版Putty，把该exe放在任意path目录中就可以在命令行中直接使用指令来使用了！

#### 软件架构
使用C#编写，只需要通过命令行输入串口端口和波特率，即可进入终端模式


#### 安装教程

1.  下载VisualStudio，下载本工程，打开VS工程并编译（已上传编译后的exe文件，如有需要可以直接使用该exe文件）
2.  将该exe重新命名，但是不要改变其后缀，这里我以ctty.exe为例
3.  将该exe文件放在一个短路径的一个文件夹里，这里以D：\document\ctty.exe为例
4.  将D:\document\设置为path路径，这样，在命令行中就可以直接输入ctty进入串口终端了，附上几张图
5.  ![image-20200210103552186](D:\Project\VisualStudio\Ctty\README.assets\image-20200210103552186.png)![image-20200210103611889](D:\Project\VisualStudio\Ctty\README.assets\image-20200210103611889.png)![image-20200210103626576](D:\Project\VisualStudio\Ctty\README.assets\image-20200210103626576.png)

#### 使用说明

1.  安装完成后在命令行里输入ctty(这个因你改的程序名而异)
2.  如果你的电脑上只有一个串口则直接回车即可，如果检测到多个串口则输入你的串口端口名再回车即可
3.  输入波特率，默认是115200，如果需要115200的波特率则直接回车，如果使用别的波特率则输入波特率再回车
4.  回车后就进入终端了，然后就可以愉快地玩耍了

#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 码云特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  码云官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解码云上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是码云最有价值开源项目，是码云综合评定出的优秀开源项目
5.  码云官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  码云封面人物是一档用来展示码云会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)

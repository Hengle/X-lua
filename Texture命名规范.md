#Android/IOS - N的2次方
    1.RGBA - ETC2 8bit/PVRTC4
    2.RGB  - ETC4/PVRTC4
#非N的2次方贴图 - 美术直接使用16bit绘图,避免16bit格式造成的色彩损失
    1.RGBA - RBGA 16bit
    2.RBG  - RGB 16bit
#像素尺寸不够N的2次方,可考虑补充像素
#图集最大尺寸2048
#光照贴图考虑直接压缩尺寸,例如1024压缩到512


#-----------------------GUI[No MipMap]
UI_RGB: RGB不透明贴图
UI_A:   透明贴图
UV_RGB: 用于滚UV的贴图
UV_A:   ...同上
>图集贴图命名,依不同情况而定.

#-----------------------场景贴图[MipMap]
Env_RGB:    RGB物件贴图
Env_A:      透明物件贴图[草,布料,叶子等]
Env_DB:     地表贴图,如T4M生成的贴图
Env_N:      法线贴图
>小贴图考虑合并成大贴图,同类型合并,例:RGB同类型合并

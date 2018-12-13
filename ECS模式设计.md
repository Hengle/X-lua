#各种实体的构造方法
#组件(Component)纯结构
#系统(System)纯函数集
#实体(Enity)组件集合


#实体,组建类型编号唯一
#实体需要引用计数,也可以改为lua weaktable

#注册结构参考lovetoys:Component


##问题?
#-----组件注册:便于组件调用
>组件结构自动生成,注册?
>是否还会存在手动生成结构与注册?
#-----事件注册:直接函数调用
>添加监听,移除监听仍以字符串形式
>触发监听,以函数形式调用
#-----执行顺序:将有顺序要求的系统成组
#-----筛选Entity:做缓存筛选,监听Add,Remove避免无用遍历

#-----固定不变的描述
>>Component结构
>>Entity匹配条件
>>System处理逻辑
#-----仅对变动的实体做处理
>>System处理的过程中,筛选变动的Entity

######--------------Component
>使用id做[类型]唯一标识
#id:类型唯一标识


######--------------Entity
>>>>Entity只关注Component,添加,移除,更新
>>>>Entity自身被引用,被销毁事件
>>>>如何初始化不同实体?数据驱动?
#id:Entity实例唯一标识
#enable:Entity是否激活
#AddComponent
#RemoveComponent
#...


######--------------System
>>>>System只关注Entity,添加,移除,更新[携带指定组件的Entity]
>>>>World内System独一无二
>>>>事件仅对实体进行一次操作,同帧内!
#mather:System所关注的组件类型要求
#active:System是否激活

######--------------SystemGroup
>>>>System成组,以添加顺序为执行顺序
#Add:顺序添加System
#Init:初始化
#OnUpdate:更新
#Destroy:销毁




######--------------Filter
>>>>为System定义筛选Components的要求
#All:包含指定的所有类型Component
#Any:包含指定类型组件中的任何一个Component


######--------------World
>>>>管理System和Entity,添加,移除,更新
#entities:Entity集合[array]
#systems:System集合[array]


######--------------AERC
>>>>自动实体引用收集,完成实体资源的释放


######--------------Pool
>>>>实体缓存池



######--------------Group
>>>>System -> Group
>>>>World  -> Groups ->  实时更新到最新,提供给System使用!
>>>>Group  -> Filter
>>>>Filter = {c1, c2, c3} -> 基于id大小排序,从小到大组合成字符串,然后hash得到最终结果
#filter:匹配条件
#filterName:唯一标识[直接使用]






#---事件种类!
>添加Component      -checkRequile : World - 维护Group
>移除Component      -checkRequile : World - 维护Group
>更新Component      -?
>添加Entity         -checkRequile : World - 维护Group
>移除Entity         -checkRequile : World - 维护Group




#------------步骤!
1.Entity初始化
2.World中添加移除Entity,System,不在循环过程中处理,在循环开始时处理
3.通知系统,处理收集到通知信息[通知列表]
4.System更新只对单个Entity触发一次
5.系统功能延迟执行,例如:技能是否打中人物等碰撞检测,延迟到当前loop结尾执行最好
6.loop 中的处理逻辑
    -system.update
    -loopEnd
        a.系统通知处理,指明处理实体[e-s]
        b.系统添加移除处理
        c.实体添加移除处理


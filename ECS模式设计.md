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




######--------------Component
>使用id做[类型]唯一标识
#id:类型唯一标识


######--------------Entity
>>>>Entity只关注Component,添加,移除,更新
>>>>Entity自身被引用,被销毁事件
#id:Entity实例唯一标识
#enable:Entity是否激活
#AddComponent
#RemoveComponent
#...


######--------------System
>>>>System只关注Entity,添加,移除,更新[携带指定组件的Entity]
>>>>World内System独一无二
#mather:System所关注的组件类型要求
#active:System是否激活

######--------------SystemGroup
>>>>System成组,以添加顺序为执行顺序
#Add:顺序添加System
#Init:初始化
#OnUpdate:更新
#Destroy:销毁




######--------------Matcher
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







#各种实体的构造方法
#组件(Component)纯结构
#系统(System)纯函数集
#实体(Enity)组件集合


#实体,组建类型编号唯一
#实体需要引用计数




######--------------Entity
>>>>Entity只关注Component,添加,移除,更新
#id:Entity唯一标识
#AddComponent
#RemoveComponent
#...


######--------------System
>>>>System只负责处理所关注的Components[1+]
#mather:System所关注的组件类型要求


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



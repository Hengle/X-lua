#--------------对范围内每个对象投射跟踪子弹;
#--------------1.Target:获取范围内对象
#--------------2.Target:单个目标对象
	"ActOnTargets"
        {
            "Target"
            {
                "Center" "CASTER"
                "Radius" "%radius"
                "Teams" "DOTA_UNIT_TARGET_TEAM_ENEMY" 
                "Types" "DOTA_UNIT_TARGET_BASIC | DOTA_UNIT_TARGET_HERO"
            }
            "Action"
            {
                "TrackingProjectile"
                {
                    "Target" "TARGET"
                    "EffectName" "particles/units/heroes/hero_enchantress/enchantress_impetus.vpcf"
                    "Dodgeable" "1"
                    "ProvidesVision" "1"
                    "VisionRadius" "300"
                    "MoveSpeed" "1000"
                    "SourceAttachment" "DOTA_PROJECTILE_ATTACHMENT_ATTACK_1"
                }
            }
        }
========================================================================================================================
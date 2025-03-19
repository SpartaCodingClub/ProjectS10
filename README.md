# 💀 전생했더니 해골이었던 건에 대하여~
채광을 통해 각종 건물을 건설하면서 던전을 지켜내는 게임에 대하여~

---

## 📂 프로젝트 스크립트 구조

 ┣ 📂 Buildings                # 건물 관련 디렉토리

 ┃ ┣ 📜 BuildingAnimation.cs   # 건물 생성/철거 애니메이션 - 이수

 ┃ ┣ 📜 BuildingBase.cs        # 체력, 생성, 철거 로직 포함한 건물 공통 베이스 - 이수

 ┃ ┣ 📜 BuildingTent.cs        # 체력 회복 막사 생성 - 이수
 
 ┃ ┣ 📜 BuildingTurret.cs      # 포탑 생성 및 공격 기능 - 이수

 ┃ ┣ 📜 BuildingWall.cs        # 벽 생성 및 방어 기능 - 이수

 ┃ ┣ 📜 BuildingWell.cs        # 식수 게이지 회복 우물 생성 - 이수

 ┃ ┣ 📜 TurretRotation.cs      # 포탑 색적 기능 - 이수

 ┃ ┣ 📜 TurretShooting.cs      # 포탑 투사체 발사 기능 - 이수
 

 ┣ 📂 Enemies                  # 적 관련 디렉토리

 ┃ ┣ 📜 EnemyController.cs     # 적 행동 패턴 - 정형권
 
 ┃ ┣ 📜 EnemyManager.cs        # 적 소환 관리 - 정형권

 ┃ ┣ 📜 EnemyStat.cs           # 적 기본 스탯 - 정형권


 ┣ 📂 Handlers                 # 핸들러 관련 디렉토리

 ┃ ┣ 📜 AudioSourceHandler.cs  # 3D 오디오 소스 제어 - 송석호

 ┃ ┣ 📜 ProjectileHandler.cs   # 투사체 제어 - 박성준
 
 ┃ ┣ 📜 SequenceHandler.cs     # 시퀀스 제어 - 송석호
 
 ┃ ┣ 📜 StatHandler.cs         # 캐릭터 스탯 관리 - 박성준, 정형권
 

 ┣ 📂 Items                    # 아이템 관련 디렉토리

 ┃ ┣ 📜 Item.cs                # 아이템 베이스 스크립트 - 박성준

 ┃ ┣ 📜 ItemData.cs            # 아이템 데이터 스크립터블 오브젝트 - 박성준

 ┃ ┣ 📜 ItemInspector.cs       # Item.cs 의 커스텀 인스펙터 - 박성준

 ┃ ┣ 📜 Projectile.cs          # 발사체 스크립트 - 박성준

 ┃ ┣ 📜 Weapon.cs              # 무기 스크립트 - 박성준
 

 ┣ 📂 Managers                 # 매니저 관련 디렉토리
 
 ┃ ┣ 📂 MapManager             # 게임 맵 관련 디렉토리
 
 ┃ ┃ ┣ 📜 Map.cs               # 동적 맵 생성 - 송석호
 
 ┃ ┃ ┣ 📜 Map_Block.cs         # 동적 맵 벽 - 송석호

 ┃ ┃ ┣ 📜 Map_Tile.cs          # 동적 맵 타일 - 송석호
 
 ┃ ┣ 📂 PoolManager            # 오브젝트 풀링 관련 디렉토리

 ┃ ┃ ┣ 📜 Pool.cs              # Poolable 오브젝트를 관리하는 스크립트 - 송석호

 ┃ ┃ ┣ 📜 Poolable.cs          # 풀링이 가능한 오브젝트 - 송석호

 ┃ ┃ ┣ 📜 PoolManager.cs       # 오브젝트 풀링 관리 매니저 - 송석호
 
 ┃ ┣ 📂 UIManager              # 게임 UI 관련 디렉토리

 ┃ ┃ ┣ 📜 UI_Base.cs           # UI 베이스 오브젝트 - 송석호

 ┃ ┃ ┣ 📜 UI_Menu.cs           # 메뉴 UI 베이스 - 송석호

 ┃ ┃ ┣ 📜 UI_Scene.cs          # 씬 UI 베이스 - 송석호

 ┃ ┃ ┣ 📜 UI_SubItem.cs        # 보조 UI 베이스 - 송석호

 ┃ ┃ ┣ 📜 UI_WorldSpace.cs     # 월드 좌표계 UI 베이스 - 송석호

 ┃ ┃ ┣ 📜 UIManager.cs         # UI 관리 매니저 - 송석호

 ┃ ┣ 📜 AudioManager.cs        # 오디오 관리 매니저 - 송석호

 ┃ ┣ 📜 BuildingManager.cs     # 건물 배치 및 철거 관리 매니저 - 이수

 ┃ ┣ 📜 GameManager.cs         # 게임 로직 관리 매니저 - 송석호, 박성준, 정형권, 박승규

 ┃ ┣ 📜 ItemManager.cs         # 아이템 관리 매니저 - 송석호

 ┃ ┣ 📜 Managers.cs            # 싱글톤 매니저 - 송석호

 ┃ ┣ 📜 ResourceManager.cs     # 리소스 로드 및 풀링 관리 매니저 - 송석호
 

 ┣ 📂 Objects                  # 오브젝트 관련 디렉토리

 ┃ ┣ 📜 InteractableObject.cs  # 상호작용 베이스 스크립트 - 박성준, 박승규

 ┃ ┣ 📜 MiningResource.cs      # 오브젝트기본값, 상호작용했을때 호출되는 스크립트 - 박승규


 ┣ 📂 Player                   # 플레이어 관련 디렉토리

 ┃ ┣ 📜 FootStep.cs            # 플레이어 발소리 재생 - 박성준

 ┃ ┣ 📜 P_Action.cs            # 플레이어 동작 대기 리스트 - 박성준

 ┃ ┣ 📜 P_AniHandler.cs        # 플레이어 애니메이션 조작 - 박성준

 ┃ ┣ 📜 P_CameraController.cs  # 카메라 조작 - 박성준

 ┃ ┣ 📜 P_Controller.cs        # 플레이어 메인 스크립트 및 움직임 - 박성준

 ┃ ┣ 📜 P_Equipment.cs         # 플레이어 장비 - 박성준

 ┃ ┣ 📜 P_Interaction.cs       # 플레이어 상호작용 - 박성준

 ┃ ┣ 📜 P_InteractionFinder.cs # 플레이어 상호작용 보조 스크립트 - 박성준

 ┃ ┣ 📜 P_Stat.cs              # 플레이어 능력치 - 박성준

 
 ┣ 📂 UI                       # UI 관련 디렉토리 - 송석호

 ┃ ┣ 📂 UI_Build               # Build UI 관련 디렉토리

 ┃ ┣ 📂 UI_Inventory           # Inventory UI 관련 디렉토리


 ┣ 📂 Utilities                # 유틸 관련 디렉토리

 ┃ ┣ 📜 Debug.cs               # Debug 랩핑 - 송석호

 ┃ ┣ 📜 Define.cs              # 상수 선언 - 송석호

 ┃ ┣ 📜 Extention.cs           # 확장 메서드 관리 - 송석호

 ┃ ┣ 📜 Utility.cs             # 게임 유틸 관리 - 송석호
 

---

## 🎮 인게임 구성

맵 제작, 매니저 및 프레임워크 - 송석호

카메라, 플레이어 (상호작용) 및 게임 로직 - 박성준

적 행동 패턴 및 생성 - 정형권

건축 시스템 및 건물 기능 구현 - 이 수

자원 동적 생성 및 풀링 - 박승규

---

## ⚙ 주요 시스템
** 조작법 **
- "W A S D" 를 이용하여 플레이어 이동
- "마우스"를 이용하여 플레이어 회전
- "마우스 휠" 확대 및 축소
- "E" 상호작용
- "B" 건축 리스트 표시

** 건물 **
- 텐트 - 상호작용 키를 눌러 체력 회복 가능
- 우물 - 상호작용 키를 눌러 목마름 회복 가능
- 벽 - 적 이동 방해
- 포탑 - 가장 가까운 적을 목표로 자동 공격
- 드레곤 포탑 - 가장 가까운 적을 목표로 강력한 자동 공격

** 적 **
-기사 - 짧은 근접 공격, 일반등급
-창기사 - 조금 긴 근접 공격, 일반등급
-궁수 - 원거리 공격, 일반등급
-마법사 - 원거리 공격, 일반등급
-기사단장 - 짧은 근접 공격, 체력이 일반등급 적 보다 많다
-기마병 - 긴 근접 공격, 중간보스, 빠른 속도로 달려와 공격
-드레곤라이더 - 긴 원거리 공격, 최종 보스, 멀리서 화염을 발사

** 자원 **
- 파란 보석 - 기본 자원으로 건축에 사용
- 보라 보석 - 기본 자원으로 건축에 사용
- 빨간 보석 - 희귀 자원으로 드레곤 포탑 건축에 사용

---

## 🎥 플레이 영상
https://github.com/user-attachments/assets/ae9f1881-59e7-4953-b4bc-be4c69f006f7

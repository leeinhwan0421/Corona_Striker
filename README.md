# Corona Striker
![Preview-1](https://github.com/user-attachments/assets/32a2ceeb-b3a4-45ad-afd7-33101283b267)
> 2022 지방기능경기대회 게임개발 부분 금상 작품입니다.  
>    
> 백신 로봇을 활용하여 환자의 몸 속으로 들어가 코로나-19 바이러스에 감염된 혈관을 치료하는 내용의 게임입니다.

# 플레이 화면
![Preview-2](https://github.com/user-attachments/assets/f483344b-12ce-4157-96f5-89440b4b5e21)

# 조작법
![Control](https://github.com/user-attachments/assets/47cf93cb-4b4d-4e01-a7b1-6ba6a379705c)

# 기믹 설명

### 플레이어 설명
---
| **이름** | **조작 키** |**설명** |
|----------|----------|----------|
| 이동   | WASD     | 플레이어를 상하좌우로 이동시킵니다. |
| 공격    |   Space   | 화면 위쪽으로 공격을 발사합니다. |
| 차지 샷   | X     | 적을 향해 유도되는 총알을 충전 된 만큼 발사합니다. (모티브: 롤 카이사 Q) |
| 폭탄    | C     | 적의 총알을 막아주고 적에게 닿을 시 데미지를 주는 오브젝트를 느린 속도로 발사합니다.(모티브: 메이플 스토리 플레임 위자드의 스킬 오비탈 플레임 IV) |

### 오브젝트 설명
---
오브젝트를 Sprite Renderer의 color 값을 통해 구분 하였기에 인게임 스크린샷으로 제공하겠습니다.
| **이름** | **프리뷰** | **설명** |
|----------|----------|----------|
| 백혈구    | ![image](https://github.com/user-attachments/assets/667c18cc-3ccb-412d-9242-e4b7e5606c63)     | 플레이어와 닿거나 플레이어 총알에 맞으면 파괴되며 아이템을 드랍합니다.     |
| 적혈구    | ![image](https://github.com/user-attachments/assets/125b5a67-4c7a-43c7-b159-82d144729b4b)     | 플레이어와 닿거나 적 총알에 피격당할 경우 파괴되며, 환자의 고통을 10 증가시킵니다.     |

### 적 설명
---
1 스테이지에서는 바이러스가 변이되지 않은 상태로 등장하게 됩니다.  
2 스테이지에서는 모든 바이러스가 변이된 상태로 등장하게 됩니다.

| **이름**       | **프리뷰**                                                                 | **설명**                                    | **변이**                        |
|----------------|---------------------------------------------------------------------------|--------------------------------------------|--------------------------------|
| 박테리아      | ![박테리아](https://github.com/user-attachments/assets/8da3cdb0-99a6-415e-bec8-3cbb0281ec4c) | 플레이어 주변을 돌다가 일정 시간 이후 혈관으로 들어간다. | 플레이어 위에서 총알을 뿌려댄다. |
| 바이러스      | ![바이러스](https://github.com/user-attachments/assets/09b7b9b9-ae2d-4f4a-b4a2-3480d8c1a13d) | 플레이어를 향해 약한 공격력을 지닌 탄을 쏜다.  | 플레이어를 향해 더욱 강한 공격력을 지닌 탄을 빠른 속도로 쏜다.                           |
| 암세포        | ![암세포](https://github.com/user-attachments/assets/adfc87dd-e89c-4365-9008-d001ce0b89f6)   | 강한 탄을 맵 전체에 산탄 형식으로 뿌린다.                   | 더욱 강한 공격을 플레이어 양 옆으로 발사한다.                          |
| SARS-Cov-2    | ![SARS-Cov-2](https://github.com/user-attachments/assets/180c4c73-cc07-429a-a2d3-127cf8335561) | 플레이어를 향해 매운 강한 탄을 쏘며, 맵 전체에 탄막을 형성한다. 바이러스 및 박테리아를 복제해 살포한다.                   | 공격의 빈도가 잦아지며, 더욱 강해진다. 또한 패턴이 강화된다.                          |

### 아이템 설명
---
아이템을 Sprite Editor를 통해 잘랐기에 transparent 이미지가 아닙니다. (인게임 스크린샷으로 제공)
| **이름** | **프리뷰** | **설명** |
|----------|----------|----------|
| 레벨 업     | ![image](https://github.com/user-attachments/assets/328be2ad-c9eb-43d4-b9ec-371271690bd4)     | 플레이어의 공격 레벨을 영구적으로 1 올린다.     |
| 무적 방패     | ![image](https://github.com/user-attachments/assets/7bf1bac7-ec5d-4c4b-a4f4-0566f2bc698f)     | 플레이어가 3초동안 모든 피해에 면역이 된다.     |
| 나노 서포트 로봇     | ![image](https://github.com/user-attachments/assets/d6429ea7-ad4c-48a0-8264-fc806a7e1e4d)     | 플레이어 양 옆으로 공격하는 로봇 두 기가 소환된다.     |
| 신성한 책     | ![image](https://github.com/user-attachments/assets/b8896c13-1ff4-42f9-b8b3-4f27e51803ab)     | 모든 적에게 (보스 포함) 큰 데미지를 준다.     |
| 진통제     | ![image](https://github.com/user-attachments/assets/99a5730b-0b82-44a1-a2ca-9467863d562a)     |  환자의 고통 지수를 20 낮추어준다.    |
| 회복 물약    | ![image](https://github.com/user-attachments/assets/74fc5509-fe0c-414a-b467-5af3c541ab02)     | 나노 로봇의 체력 지수를 20 높여준다.     |

# 시연 영상
[YouTube 링크](https://www.youtube.com/watch?v=PHmythXdy-I&t=28s)

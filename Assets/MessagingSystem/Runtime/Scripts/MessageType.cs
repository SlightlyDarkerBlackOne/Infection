public enum MessageType
{
	#region Editor

	ChangeColor = 10,
	ChangeCubeColor = 20,
	ChangeCubeToColor = 30,
	ChangeSphereColor = 40,
	ChangeCylinderColor = 50,
	SemaphoreTest = 60,
	Log = 70,

	#endregion

	#region Input

	ActionTriggered = 80,
	TogglePause = 90,
	GamePaused = 100,
	HIDRegained = 110,
	HIDDisconnected = 120,
	MenuButtonOptionSelected = 130,
	PinchGesture = 140,
	RebindingStarted = 150,
	RebindingCancelled = 160,
	RebindingCompleted = 170,
	TouchStarted = 180,
	TouchEnded = 190,
	TouchMoving = 200,
	TouchDeltaZero = 201,
	Swipe = 210,
	ControlsChanged = 220,
	InitTouchJoystick = 230,
	PanGesture = 240,

	#endregion

	#region playfab

	PFLogin = 250,
	PFLogout = 260,
	PFRegister = 270,
	PFUpdateFilePolicy = 280,
	PFUpdateUserData = 290,
	PFUpdatePlayerStatistics = 300,
	PFGetPlayerStatistics = 310,
	PFError = 320,
	PFGetFiles = 330,
	PFDownloadFile = 340,
	PEUploadDetails = 350,
	PEFileUploaded = 360,
	PEAbortUpload = 370,
	PFGetUserData = 380,
	PFCloudScript = 390,
	PFFunction = 400,
	PFConsumeItem = 410,
	PFPurchaseItem = 420,
	PFGetCatalog = 430,
	PFGetInventory = 440,
	PFAddCurrency = 450,
	PFAddCatalogItem = 460,
	PFRemoveCatalogItem = 470,

	#endregion

	#region Abilities

	HealthChanged = 480,
	AbilityTriggered = 490,
	MeleeAttack = 500,
	MeleeAttackSpecial = 510,
	PrimaryAbility = 520,
	SpecialAbility = 530,
	DashAbility = 540,
	AutoAttackAbilityTriggered = 550,
	AbilityAim = 551,
	CancelAbility = 552,
	AbilityStarted = 553,

	#endregion

	#region Build

	SetBuildMode = 560,
	SaveSuccesful = 570,

	#endregion

	#region Scene

	ChangeScene = 580,
	ReloadScene = 590,
	SceneLoaded = 600,
	AddLoadObject = 610,
	MapLoaded = 620,
	PlayerSpawned = 630,
	LevelWin = 640,
	PlayerDied = 650,
	BotDied = 660,
	UpdateHUD = 670,
	DestroyJoystick = 680,
	ServerComStart = 690,
	ServerComEnd = 700,
	PauseLoading = 710,
	ContinueLoading = 720,

	#endregion

	#region Settings

	SetQuality = 730,

	#endregion

	#region Inventory

	GetGenomes = 740,

	#endregion

	#region Animations

	ChangeRunSpeedModifier = 750,
	ChangePrimaryAttackModifier = 760,
	ChangeSpecialAttackModifier = 770,

	#endregion

	#region Targeting

	TargetEnemyAcquired = 780,
	TargetEnemyLost = 790,
	AbilityPrimed = 791,
	AbilityComplete = 792,
	AbilityCancel = 793,

	#endregion

	#region AttackType

	OnAttackTypeChange = 800,
	WeaponTypeChange = 810,
	#endregion

}
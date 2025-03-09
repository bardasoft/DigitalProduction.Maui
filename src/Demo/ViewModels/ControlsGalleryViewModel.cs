﻿using DigitalProduction.Demo.Models;

namespace DigitalProduction.Demo.ViewModels;

public partial class ControlsGalleryViewModel() : BaseGalleryViewModel(
[
	SectionModel.Create<DynamicMenusPageViewModel>("Dynamic Menus", "Add menu flyouts dynamically."),
	SectionModel.Create<RecentlyUsedMenuPageViewModel>("Recently Used Menu", "A recently used menu flyout."),
	SectionModel.Create<StepperPageViewModel>("Stepper Control", "An alternate stepper control."),
	SectionModel.Create<SaveFilePickerPageViewModel>("Save File Picker Control", "An alternate stepper control.")
]);
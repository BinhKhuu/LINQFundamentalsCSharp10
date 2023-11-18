using LINQSamples;

// Create instance of view model
SamplesViewModel vm = new();

// Call Sample Method
//var result = vm.ForEachQuery();
//var result = vm.ForEachMethod();
//var result = vm.ForEachSubQueryMethod();
//var result = vm.ForEachSubQueryQuery();
//var result = vm.ForEachQueryCallingMethodQuery();
var result = vm.ForEachQueryCallingMethod();

// Display Results
vm.Display(result);
using LINQSamples;

// Create instance of view model
SamplesViewModel vm = new();

// Call Sample Method
//var result = vm.CountQuery();
//var result = vm.CountMethod();
//var result = vm.CountFilteredMethod();
//var result = vm.CountFilteredQuery();
//var result = vm.MinQuery();
//var result = vm.MinMethod();
//var result = vm.MaxMethod();
//var result = vm.MaxQuery();
//var result = vm.MinByQuery();
//var result = vm.MinByMethod();
//var result = vm.MaxByQuery();
//var result = vm.MaxByMethod();
//var result = vm.SumQuery();
//var result = vm.SumMethod();
//var result = vm.AverageMethod();
//var result = vm.AverageQuery();
//var result = vm.SumQuery();
//var result = vm.AggregateCustomQuery();
//var result = vm.AggregateCustomMethod();
//var result = vm.AggregateUsingGroupByQuery();
//var result = vm.AggregateUsingGroupByMethod();
var result = vm.AggregateMoreEfficientMethod();

// Display Results
vm.Display(result);
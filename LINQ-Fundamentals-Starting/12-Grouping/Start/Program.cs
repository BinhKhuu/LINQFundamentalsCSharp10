using LINQSamples;

// Create instance of view model
SamplesViewModel vm = new();

// Call Sample Method
//var result = vm.GroupByQuery();
//var result = vm.GroupByMethod();
//var result = vm.GroupByUsingKeyQuery();
//var result = vm.GroupByWhereQuery();
//var result = vm.GroupByWhereMethod();
//var result = vm.GroupBySubQueryQuery();
//var result = vm.GroupBySubQueryMethod();
//var result = vm.GroupByDistinctQuery();
var result = vm.GroupByDistinctMethod();
// Display Results
vm.Display(result);
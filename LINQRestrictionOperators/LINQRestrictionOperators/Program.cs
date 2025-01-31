
using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq_EFDay01
{
    class Program
    {
        static void Main()
        {
            // 1. Find all products that are out of stock
            var outOfStockProducts = ListGenerators.ProductList.Where(p => p.UnitsInStock == 0);
            Console.WriteLine("Out of Stock Products:");
            foreach (var product in outOfStockProducts)
                Console.WriteLine(product);

            // 2. Find all products that are in stock and cost more than 3.00 per unit
            var expensiveInStockProducts = ListGenerators.ProductList
                .Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00m);
            Console.WriteLine("\nIn Stock & Cost More than 3.00:");
            foreach (var product in expensiveInStockProducts)
                Console.WriteLine(product);

            // 3. Returns digits whose name is shorter than their value
            string[] Arr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortNames = Arr.Where((name, index) => name.Length < index);
            Console.WriteLine("\nDigits with name shorter than value:");
            foreach (var digit in shortNames)
                Console.WriteLine(digit);

            // 4. Get first product out of stock
            var firstOutOfStock = ListGenerators.ProductList.FirstOrDefault(p => p.UnitsInStock == 0);
            Console.WriteLine("\nFirst product out of stock:");
            Console.WriteLine("No product found");

            // 5. Return the first product whose Price > 1000, unless there is no match
            var expensiveProduct = ListGenerators.ProductList.FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine("\nFirst product with price > 1000:");
            Console.WriteLine("No product found");

            // 6. Retrieve the second number greater than 5
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondGreaterThanFive = numbers.Where(n => n > 5).Skip(1).FirstOrDefault();
            Console.WriteLine("\nSecond number greater than 5: " + secondGreaterThanFive);

            // 7. Count odd numbers in the array
            var oddCount = numbers.Count(n => n % 2 == 1);
            Console.WriteLine("\nCount of odd numbers: " + oddCount);

            // 8. Return a list of customers and how many orders each has
            var customerOrders = ListGenerators.CustomerList
                .Select(c => new { c.Name, OrderCount = c.Orders.Length });
            Console.WriteLine("\nCustomers and their orders count:");
            foreach (var customer in customerOrders)
                Console.WriteLine($"{customer.Name}: {customer.OrderCount} orders");

            // 9. Return a list of categories and how many products each has
            var categoryCounts = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() });
            Console.WriteLine("\nCategories and product counts:");
            foreach (var category in categoryCounts)
                Console.WriteLine($"{category.Category}: {category.Count} products");

            // 10. Get the total of the numbers in an array
            var totalSum = numbers.Sum();
            Console.WriteLine("\nTotal sum of numbers: " + totalSum);

            // Read dictionary_english.txt into an array
            // string[] dictionaryWords = File.ReadAllLines("dictionary_english.txt");

            /* 5. Get the total number of characters of all words in dictionary_english.txt
            var totalCharacters = dictionaryWords.Sum(word => word.Length);
            Console.WriteLine("\nTotal number of characters in dictionary: " + totalCharacters);*/

            // 6. Get the total units in stock for each product category
            var totalUnitsInStock = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, TotalUnits = g.Sum(p => p.UnitsInStock) });
            Console.WriteLine("\nTotal units in stock per category:");
            foreach (var category in totalUnitsInStock)
                Console.WriteLine($"{category.Category}: {category.TotalUnits} units");

            /* 7. Get the length of the shortest word in dictionary_english.txt
            var shortestWordLength = dictionaryWords.Min(word => word.Length);
            Console.WriteLine("\nLength of the shortest word: " + shortestWordLength);*/

            // 8. Get the cheapest price among each category's products
            var cheapestPricePerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, MinPrice = g.Min(p => p.UnitPrice) });
            Console.WriteLine("\nCheapest price per category:");
            foreach (var category in cheapestPricePerCategory)
                Console.WriteLine($"{category.Category}: ${category.MinPrice}");

            // 9. Get the products with the cheapest price in each category (Use Let)
            var cheapestProductsPerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    CheapestProducts = g.Where(p => p.UnitPrice == g.Min(x => x.UnitPrice))
                });
            Console.WriteLine("\nCheapest products per category:");
            foreach (var category in cheapestProductsPerCategory)
            {
                Console.WriteLine($"{category.Category}:");
                foreach (var product in category.CheapestProducts)
                    Console.WriteLine($"  {product.ProductName}: ${product.UnitPrice}");
            }

            /* 10. Get the length of the longest word in dictionary_english.txt
            var longestWordLength = dictionaryWords.Max(word => word.Length);
            Console.WriteLine("\nLength of the longest word: " + longestWordLength);*/

            // 11. Get the most expensive price among each category's products
            var mostExpensivePricePerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, MaxPrice = g.Max(p => p.UnitPrice) });
            Console.WriteLine("\nMost expensive price per category:");
            foreach (var category in mostExpensivePricePerCategory)
                Console.WriteLine($"{category.Category}: ${category.MaxPrice}");

            // 12. Get the products with the most expensive price in each category
            var mostExpensiveProductsPerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    ExpensiveProducts = g.Where(p => p.UnitPrice == g.Max(x => x.UnitPrice))
                });
            Console.WriteLine("\nMost expensive products per category:");
            foreach (var category in mostExpensiveProductsPerCategory)
            {
                Console.WriteLine($"{category.Category}:");
                foreach (var product in category.ExpensiveProducts)
                    Console.WriteLine($"  {product.ProductName}: ${product.UnitPrice}");
            }

            /*13. Get the average length of the words in dictionary_english.txt
            var averageWordLength = dictionaryWords.Average(word => word.Length);
            Console.WriteLine("\nAverage word length in dictionary: " + averageWordLength);*/

            // 14. Get the average price of each category's products
            var averagePricePerCategory = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, AvgPrice = g.Average(p => p.UnitPrice) });
            Console.WriteLine("\nAverage price per category:");
            foreach (var category in averagePricePerCategory)
                Console.WriteLine($"{category.Category}: ${category.AvgPrice:F2}");

            // Sort a list of products by name
            var sortedProductsByName = ListGenerators.ProductList.OrderBy(p => p.ProductName);
            Console.WriteLine("\nProducts sorted by name:");
            foreach (var product in sortedProductsByName)
                Console.WriteLine(product);

            // Uses a custom comparer to do a case-insensitive sort of the words in an array
            string[] wordsArr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords = wordsArr.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\nWords sorted case-insensitively:");
            foreach (var word in sortedWords)
                Console.WriteLine(word);

            // Sort a list of products by units in stock from highest to lowest
            var sortedByStock = ListGenerators.ProductList.OrderByDescending(p => p.UnitsInStock);
            Console.WriteLine("\nProducts sorted by units in stock:");
            foreach (var product in sortedByStock)
                Console.WriteLine(product);

            // Sort a list of digits, first by length of their name, and then alphabetically
            string[] digitsArr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits = digitsArr.OrderBy(d => d.Length).ThenBy(d => d);
            Console.WriteLine("\nDigits sorted by length and alphabetically:");
            foreach (var digit in sortedDigits)
                Console.WriteLine(digit);

            // Sort first by word length and then by a case-insensitive sort of the words in an array
            var sortedWordsByLength = wordsArr.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\nWords sorted by length and case-insensitively:");
            foreach (var word in sortedWordsByLength)
                Console.WriteLine(word);

            // Sort a list of products, first by category, then by unit price from highest to lowest
            var sortedByCategoryAndPrice = ListGenerators.ProductList
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.UnitPrice);
            Console.WriteLine("\nProducts sorted by category and unit price (desc):");
            foreach (var product in sortedByCategoryAndPrice)
                Console.WriteLine(product);

            // Sort first by word length and then by a case-insensitive descending sort of the words in an array
            var sortedWordsDesc = wordsArr.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\nWords sorted by length and case-insensitively descending:");
            foreach (var word in sortedWordsDesc)
                Console.WriteLine(word);

            // Create a list of all digits in the array whose second letter is 'i' that is reversed from the order in the original array
            var filteredDigits = digitsArr.Where(d => d.Length > 1 && d[1] == 'i').Reverse();
            Console.WriteLine("\nDigits with second letter 'i' in reverse order:");
            foreach (var digit in filteredDigits)
                Console.WriteLine(digit);

            // 1. Return a sequence of just the names of a list of products
            var productNames = ListGenerators.ProductList.Select(p => p.ProductName);
            Console.WriteLine("\nProduct Names:");
            foreach (var name in productNames)
                Console.WriteLine(name);

            // 2. Produce a sequence of the uppercase and lowercase versions of each word
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var wordVariants = words.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });
            Console.WriteLine("\nUppercase and Lowercase versions:");
            foreach (var word in wordVariants)
                Console.WriteLine($"{word.Upper}, {word.Lower}");

            // 3. Produce a sequence containing some properties of Products
            var productDetails = ListGenerators.ProductList.Select(p => new { p.ProductName, Price = p.UnitPrice });
            Console.WriteLine("\nProduct Details (Name & Price):");
            foreach (var product in productDetails)
                Console.WriteLine($"{product.ProductName}: ${product.Price}");

            // 4. Determine if the value of ints in an array match their position in the array
            int[] arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var positionMatches = arr.Select((value, index) => new { Number = value, InPlace = value == index });
            Console.WriteLine("\nNumber: In-place?");
            foreach (var item in positionMatches)
                Console.WriteLine($"{item.Number}: {item.InPlace}");

            // 5. Returns all pairs of numbers from both arrays such that numbersA < numbersB
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var pairs = from a in numbersA
                        from b in numbersB
                        where a < b
                        select new { A = a, B = b };
            Console.WriteLine("\nPairs where a < b:");
            foreach (var pair in pairs)
                Console.WriteLine($"{pair.A} is less than {pair.B}");

            // 6. Select all orders where the order total is less than 500.00
            var smallOrders = ListGenerators.CustomerList
                .SelectMany(c => c.Orders)
                .Where(o => o.Total < 500.00);
            Console.WriteLine("\nOrders with total less than 500.00:");
            foreach (var order in smallOrders)
                Console.WriteLine(order);

            // 7. Select all orders where the order was made in 1998 or later
            var recentOrders = ListGenerators.CustomerList
                .SelectMany(c => c.Orders)
                .Where(o => o.OrderDate.Year >= 1998);
            Console.WriteLine("\nOrders made in 1998 or later:");
            foreach (var order in recentOrders)

                Console.WriteLine(order);

            // 1. Sort a list of products by name
            var sortedProductsByName2 = ListGenerators.ProductList.OrderBy(p => p.ProductName);
            Console.WriteLine("\nProducts sorted by name:");
            foreach (var product in sortedProductsByName)
                Console.WriteLine(product);

            // 2. Case-insensitive sort of words in an array
            string[] wordsarr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            IOrderedEnumerable<string> orderedEnumerable = wordsArr.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
            var sortedWords2 = orderedEnumerable;
            Console.WriteLine("\nWords sorted case-insensitively:");
            foreach (var word in sortedWords)
                Console.WriteLine(word);

            // 3. Sort products by units in stock from highest to lowest
            var sortedByStock2 = ListGenerators.ProductList.OrderByDescending(p => p.UnitsInStock);
            Console.WriteLine("\nProducts sorted by units in stock:");
            foreach (var product in sortedByStock)
                Console.WriteLine(product);

            // 4. Sort digits by length then alphabetically
            string[] digitsarr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits2 = digitsArr.OrderBy(d => d.Length).ThenBy(d => d);
            Console.WriteLine("\nDigits sorted by length and alphabetically:");
            foreach (var digit in sortedDigits)
                Console.WriteLine(digit);

            // 5. Sort words by length then case-insensitively
            var sortedWordsByLength2 = wordsArr.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\nWords sorted by length and case-insensitively:");
            foreach (var word in sortedWordsByLength)
                Console.WriteLine(word);

            // 6. Sort products by category then by unit price descending
            var sortedByCategoryAndPrice2 = ListGenerators.ProductList
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.UnitPrice);
            Console.WriteLine("\nProducts sorted by category and unit price (desc):");
            foreach (var product in sortedByCategoryAndPrice)
                Console.WriteLine(product);

            // 7. Sort words by length then case-insensitively descending
            var sortedWordsDesc2 = wordsArr.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\nWords sorted by length and case-insensitively descending:");
            foreach (var word in sortedWordsDesc)
                Console.WriteLine(word);

            // 8. Get the first 3 orders from customers in Washington
            var firstThreeOrders = ListGenerators.CustomerList
                .Where(c => c.Country == "Washington")
                .SelectMany(c => c.Orders)
                .Take(3);
            Console.WriteLine("\nFirst 3 orders from Washington:");
            foreach (var order in firstThreeOrders)
                Console.WriteLine(order);

            // 9. Get all but the first 2 orders from customers in Washington
            var skipTwoOrders = ListGenerators.CustomerList
                .Where(c => c.Country == "Washington")
                .SelectMany(c => c.Orders)
                .Skip(2);
            Console.WriteLine("\nOrders from Washington excluding first two:");
            foreach (var order in skipTwoOrders)
                Console.WriteLine(order);

            // 10. Determine if any words in dictionary_english.txt contain 'ei'
            string[] dictionaryWords = File.ReadAllLines("dictionary_english.txt");
            var containsEi = dictionaryWords.Any(word => word.Contains("ei"));
            Console.WriteLine("\nAny word contains 'ei': " + containsEi);

            // 11. Return a grouped list of products only for categories with at least one out of stock product
            var categoriesWithOutOfStock = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0))
                .Select(g => new { Category = g.Key, Products = g });
            Console.WriteLine("\nCategories with out of stock products:");
            foreach (var category in categoriesWithOutOfStock)
            {
                Console.WriteLine(category.Category);
                foreach (var product in category.Products)
                    Console.WriteLine($"  {product.ProductName}");
            }

            // 12. Return a grouped list of products only for categories where all products are in stock
            var categoriesAllInStock = ListGenerators.ProductList
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0))
                .Select(g => new { Category = g.Key, Products = g });
            Console.WriteLine("\nCategories with all products in stock:");
            foreach (var category in categoriesAllInStock)
            {
                Console.WriteLine(category.Category);
                foreach (var product in category.Products)
                    Console.WriteLine($"  {product.ProductName}");

            }
        }
    }
}

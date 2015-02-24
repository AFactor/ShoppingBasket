# ShoppingBasket
A console basket with TDD approach

Approach

First I wrote the entities. 
  Price entity has the product Title(identifier), line price. 
  Basket entity has Product Title, line price and quantity.
  Promotion entity has promotion detail and the Buckets. Buckets actually define product combination and promotional discount.
  
  
Next comes the repositories. One each to get Price and Promotion data. In real life this will come from a data store. 
I have mocked it for this. Repositories are created using repository pattern.

Next comes Basketservice. Basket takes concrete implementations of PriceRepository and PromotionRepository.

public BasketService(IPriceRepository priceRepository, IPromotionRepository promoRepository)

It has two public methods for setting the basket and calculating promotion.

This is the point I started writting Unit tests for basketservice. First thing is to check Setbasket for missing and invalid arguments, and then create two mocks for Price and Promotion Repositories to test other secnarios given. I have also included the testresults folder.
I have spend most time on the calculate promotion method, trying to make it as futureproof as possible by factoring in all kinds of promotion.

Once basket service is done, I set about writting the method for passing the input and writting down the output which takes an implementation of writer and repositories. The Writer is a consolewriter in the real application, and a mock writer from test which is testing whether it's being called 3 times.
I have used a small utility call appsettings to get config values in a type safe generic manner.

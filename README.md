![image](https://github.com/user-attachments/assets/cef48965-82e1-40a9-a491-15ba5c1586d1)

Що вона робить:

Get-ChildItem -Recurse -Filter "*.cs" — знаходить всі .cs файли у поточній папці та підпапках.

Get-Content — читає вміст кожного файлу.

Where-Object { $_ -match '\S' } — фільтрує рядки, залишаючи тільки ті, що містять хоча б один непробільний символ (ігнорує порожні рядки та рядки з пробілами/табуляцією).

.Count — підраховує кількість рядків, що залишилися.


**Інтерфейс користувача (UI)**
Головне меню:

Кнопки: Категорія Продукти Постачальники Аналітика Звіт у PDF Звіт у Excel

При натисканні на кнопку Категорія відкривається таблиця Категорії
Також з'являється можливість здійснювати CRUD -операції з цією таблицею:
- Додати товар
- Змінити товар
- Видалити


При натисканні на кнопку Продукти відкривається таблиця Продукти
Також з'являється можливість здійснювати CRUD -операції з цією таблицею:
- Додати товар
- Змінити товар
- Видалити
- Також є можливість сортувати:
   - Назва
   - К-сть
   - Ціна


При натисканні на кнопку Постачальники відкривається таблиця Постачальники
Також з'являється можливість здійснювати CRUD -операції з цією таблицею:
- Додати товар
- Змінити товар
- Видалити


При натисканні на кнопку Аналітика 

З'вляється вікно де відображаються основні дані аналітики:
- к-сть товарів
- середня ціна товару
- к-сть з критично низьки запасом
- відображення скільки товарів якої категорії

При натисканні на кнопку  Звіт у Excel 

Завантажується файл, який містить інформацію про товари та аналітику


При натисканні на кнопку Звіт у PDF 

Завантажується файл, який містить інформацію про товари та аналітику


***Design Patterns***

**Repository Pattern**

У проєкті реалізовано шаблон проектування Repository, який забезпечує абстракцію доступу до даних. Це дозволяє:

- Ізолювати логіку доступу до бази даних від бізнес-логіки додатку, що спрощує підтримку і тестування.

- Легко підмінювати джерело даних (наприклад, для тестування або зміни типу БД).

- Централізовано реалізувати CRUD-операції для всіх моделей через загальний клас Repository<T>, що забезпечує спільний доступ до даних для різних типів.

- Розширювати функціональність через специфічні репозиторії (наприклад, IProductRepository, ProductRepository для роботи з конкретними сутностями), що дозволяє гнучко додавати методи для окремих моделей, не змінюючи основну реалізацію.

Структура:
1) IRepository<T> – базовий інтерфейс для загальних операцій доступу до даних. [InventoryManagement/Repositories/IRepository.cs](./InventoryManagement/Interfaces/IRepository.cs)

2) Repository<T> – загальна реалізація репозиторія, що використовує IRepository<T>.  [InventoryManagement/Repositories/Repository.cs](./InventoryManagement/Repositories/Repository.cs)

3) IProductRepository, ProductRepository – приклади специфічних реалізацій для конкретних моделей, які додають бізнес-логіку.
   
 [InventoryManagement/Repositories/IProductRepository.cs](./InventoryManagement/Interfaces/IProductRepository.cs)  
  [InventoryManagement/Repositories/ProductRepository.cs](./InventoryManagement/Repositories/ProductRepository.cs)

5) Цей підхід покращує підтримку та масштабованість додатку, дозволяючи легко змінювати і розширювати систему з мінімальними змінами в коді.

**MVVM (Model-View-ViewModel)**

У проєкті реалізовано архітектурний шаблон MVVM, який розділяє логіку додатку на три основні компоненти:

- Model (Модель) – представляє структуру та логіку зберігання даних. У проєкті це класи Product, Category, Supplier та Entity Framework InventoryContext, що взаємодіє з базою даних.

 [InventoryManagement/Models/Product.cs](./InventoryManagement/Models/Product.cs)  
  [InventoryManagement/Data/InventoryContext.cs](./InventoryManagement/Data/InventoryContext.cs)

- ViewModel (Модель представлення) – містить логіку взаємодії між Model і View. Для кожної сутності (Product, Category, Supplier) створено окрему ViewModel (ProductViewModel, CategoryViewModel, SupplierViewModel), яка:
  
 [InventoryManagement/ViewModels/CategoryViewModel.cs](./InventoryManagement/ViewModels/CategoryViewModel.cs)  
  [InventoryManagement/ViewModels/ProductViewModel.cs](./InventoryManagement/ViewModels/ProductViewModel.cs)
  
   - надає дані для прив’язки до інтерфейсу користувача;

   - реалізує CRUD-операції через репозиторії;

   - забезпечує реалізацію INotifyPropertyChanged для оновлення інтерфейсу при зміні даних.

- View (Подання) – XAML-файли (ProductsView.xaml, CategoriesView.xaml, SuppliersView.xaml), які відповідають за візуальне відображення та отримання вводу від користувача. View підключені до ViewModel через Data Binding, не маючи прямого доступу до моделей або логіки обробки.
  
 [InventoryManagement/Views/CategoriesView.xaml](./InventoryManagement/Views/CategoriesView.xaml)  
  [InventoryManagement/Views/ProductsView.xaml](./InventoryManagement/Views/ProductsView.xaml)


Завдяки такому розділенню:

1) інтерфейс користувача легко змінювати незалежно від бізнес-логіки;

2) спрощується тестування і супровід коду;

3) код залишається чистим і структурованим.

**Strategy**

- Інтерфейс сортування:  
  [InventoryManagement/Sorting/ISortStrategy.cs](./InventoryManagement/ViewModels/Sorting/ISortingStrategy.cs)

- Класи-стратегії:  
  [InventoryManagement/Sorting/SortByNameStrategy.cs](./InventoryManagement/ViewModels/Sorting/SortByNameStrategy.cs)  
  [InventoryManagement/Sorting/SortByPriceStrategy.cs](./InventoryManagement/ViewModels/Sorting/SortByPriceStrategy.cs)

- Контекст сортування:  
  [InventoryManagement/Sorting/ProductSorter.cs](./InventoryManagement/ViewModels/Sorting/ProductSorter.cs)

- Інтерфейс звітів:  
  [InventoryManagement/Reports/IReportStrategy.cs](./InventoryManagement/Reports/IReportStrategy.cs)

- Реалізації звітів:  
  [InventoryManagement/Reports/ExcelReportStrategy.cs](./InventoryManagement/Reports/ExcelReportStrategy.cs)  
  [InventoryManagement/Reports/PdfReportStrategy.cs](./InventoryManagement/Reports/PdfReportStrategy.cs)


Патерн "Стратегія" дозволяє визначити сімейство алгоритмів, інкапсулювати кожен з них і зробити їх взаємозамінними. Це дозволяє змінювати поведінку програми на льоту без модифікації основного коду.

 У проєкті:
 
Використовується для сортування списку товарів за різними критеріями: назвою, ціною, кількістю.

Структура реалізації:

Інтерфейс ISortStrategy — визначає метод IEnumerable<Product> Sort(IEnumerable<Product> products)

Класи-стратегії:

SortByNameStrategy

SortByPriceStrategy

SortByQuantityStrategy

Контекст: ProductSorter, який використовує одну зі стратегій

У ViewModel — виклик SetStrategy(...) змінює логіку сортування

Також було реалізовано в іншому шматочку коду

Де: IReportStrategy та її реалізації ExcelReportStrategy і PdfReportStrategy.

Як: Ви визначили інтерфейс IReportStrategy з методом GenerateReport. Реалізації цього інтерфейсу створюють різні формати звітів (Excel, PDF). Клас ReportContext приймає об’єкт стратегії і делегує йому створення звіту.

Чому: Це дозволяє легко додавати нові формати звітів без змін у клієнтському коді. Забезпечує розширюваність і замінність алгоритмів.



**Спостерігач(Observer Pattern)**

- Клас `ProductViewModel` реалізує `INotifyPropertyChanged`:  
  [InventoryManagement/ViewModels/ProductViewModel.cs](./InventoryManagement/ViewModels/ProductViewModel.cs)


Цей шаблон дозволяє об'єктам-спостерігачам стежити за змінами стану об'єкта-суб'єкта. У WPF це основа механізму data-binding.

У проєкті:

Клас ProductViewModel реалізує INotifyPropertyChanged

Компоненти UI (наприклад, TextBox, ComboBox) автоматично оновлюються при зміні властивостей

Основний механізм:

public event PropertyChangedEventHandler PropertyChanged;

protected void OnPropertyChanged(string propertyName)
{
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

Як працює:

public Product CurrentProduct
{
    get => _currentProduct;
    set
    {
        _currentProduct = value;
        OnPropertyChanged(nameof(CurrentProduct));
    }
}

Коли властивість змінюється, викликається OnPropertyChanged, і UI автоматично підтягує нове значення.
   

***Programming principles***
   1) **Принцип єдиного обов'язку (Single Responsibility Principle, SRP):**
      
      У  реалізації патерну Repository кожен клас репозиторію відповідає лише за доступ до даних для конкретної сутності, що дозволяє нам чітко дотримуватися принципу єдиного обов'язку. Це забезпечує ізоляцію логіки доступу до даних від інших аспектів додатку та дозволяє краще організувати код. Замість того, щоб один клас виконував кілька завдань (наприклад, доступ до даних, бізнес-логіка і валідація),  створила окремі репозиторії для кожної сутності, що дозволяє кожному репозиторію зосередитися лише на виконанні операцій CRUD (створення, читання, оновлення, видалення) для відповідної таблиці в базі даних.

Як це реалізовано:
- IRepository<T>: Це базовий інтерфейс, який визначає загальні операції для роботи з базою даних для будь-якої сутності. Він включає методи, такі як Add, GetById, GetAll, Update, Delete. Цей інтерфейс визначає єдину відповідальність – абстрагувати доступ до даних для всіх сутностей.

- Repository<T>: Це загальна реалізація, яка реалізує інтерфейс IRepository<T> та містить загальні методи для роботи з базою даних. Вона не має бізнес-логіки і тільки виконує операції CRUD для конкретних сутностей.

- Специфічні репозиторії (наприклад, ProductRepository): Для кожної сутності, яка потребує специфічних операцій або методів, ми створюємо окремі репозиторії. Наприклад, ProductRepository містить методи, специфічні для роботи з продуктами, а не змішує їх з іншими сутностями. Це дозволяє кожному класу бути відповідальним лише за одну задачу – доступ до даних для конкретної сутності.
2) **DRY (Don't Repeat Yourself)**
  
Цей принцип полягає в усуненні дублювання коду, логіки або функціональності. У проєкті цей принцип реалізовано через:

Generic-репозиторій Repository<T>

Замість того, щоб писати окремо CRUD-операції (Create, Read, Update, Delete) для кожної сутності (Product, Category, Supplier), я винесла спільну логіку в універсальний клас-репозиторій:

 public class Repository<T> : IRepository<T> where T : class
 {
   protected readonly InventoryContext _context;
   protected readonly DbSet<T> _dbSet;

   public Repository(InventoryContext context)
   {
     _context = context;
     _dbSet = _context.Set<T>();
   }

   public IEnumerable<T> GetAll() => _dbSet.ToList();

   public T GetById(int id) => _dbSet.Find(id); 

   public void Add(T entity)
   {
     _dbSet.Add(entity);
     _context.SaveChanges();
   }
   public void Update(T entity)
   {
     _dbSet.Update(entity);
     _context.SaveChanges();
   }

   public void Delete(T entity)
   {
     _dbSet.Remove(entity);
     _context.SaveChanges();
   }
 }
 
У разі необхідності — наприклад, для ProductRepository — я розширюю базовий функціонал, не дублюючи загальні методи.

- Перевага: менше повторень, легше оновлювати і тестувати, зміни в логіці CRUD впливають лише на одне місце.
3) **KISS (Keep It Simple, Stupid)**
  
  Цей принцип закликає уникати зайвої складності. У вашому застосунку він реалізований через:

✅ Чітка структура проєкту

Models — лише дані (без логіки).

ViewModels — логіка для в’ю.

Views — тільки візуальне представлення (UI).

Repositories — інкапсульований доступ до БД.

Interfaces — чітке розділення контрактів.

✅ Простота у використанні WPF

Ви використовуєте двосторонню прив’язку ({Binding}) для зв’язку між View та ViewModel.

✅ Простий механізм оновлення даних

CRUD виконується простими методами: AddProduct, UpdateProduct, DeleteProduct, що не містять зайвої логіки.

Усі оновлення автоматично відображаються в UI через INotifyPropertyChanged.

4) **Принцип відкритості/закритості (OCP)**

   Для додавання нового критерію сортування (наприклад, за датою) потрібно:

- Створити новий клас (наприклад, SortByDateStrategy)

- Реалізувати ISortingStrategy

Не потрібно змінювати існуючий код

5) **L — Liskov Substitution Principle (Принцип підстановки Лісков)**

Суть: будь-який екземпляр підкласу або реалізації інтерфейсу повинен без проблем підходити замість базового типу.

У коді:

- IProductRepository і ICategoryRepository можуть мати різні реалізації (наприклад, база даних, mock-репозиторій), і AnalyticsViewModel буде з ними працювати однаково.

- Реалізовано через використання інтерфейсів.

***Refactoring Techniques***

У проєкті використовувались наступні техніки рефакторингу для покращення якості, читабельності та підтримуваності коду:

1. **Extract Class**
   
Було виділено окремі класи для репозиторіїв (ProductRepository, CategoryRepository тощо), щоб ізолювати логіку доступу до даних.

3. **Move Method**

CRUD-методи були перенесені з ViewModel у відповідні репозиторії, що дозволяє зосередити логіку доступу до БД в одному місці.

5. **Encapsulate Field**

Використано властивості (public Property { get; set; }) замість прямого доступу до полів, що забезпечує контроль над доступом до даних.

7. **Replace Magic Numbers/Strings with Constants**

Константи та ідентифікатори збережені в окремих файлах (наприклад, enum або static class), щоб уникнути "магічних значень".

8. **Use Dependency Injection**

Залежності (репозиторії) передаються через конструктори до ViewModel, що робить їх легко замінними та тестованими.

9. **Remove Dead Code**

Під час розробки видалено тимчасовий або неактуальний код, що покращило чистоту проєкту.

10. **Rename for Clarity**

Імена класів, змінних і методів були змінені для кращої зрозумілості (наприклад, AddProduct() замість BtnClick()).

11. **Винесення стратегії" (Extract Strategy)**
    Це техніка, при якій ти витягуєш змінну поведінку (наприклад, сортування) у окремий клас-стратегію.

У проєкті:
Замість того, щоб писати логіку сортування у ProductViewModel, ти виніс її в окремі класи:

- SortByNameStrategy

- SortByPriceStrategy

- SortByQuantityStrategy

Це дозволяє:

- Уникати дублювання

- Тестувати стратегії незалежно

- Додавати нові варіанти сортування, не змінюючи ProductViewModel

Це класичний приклад "витягування змінного алгоритму" — дуже добра техніка для зменшення складності.

12. **Simplifying Method Calls (Спрощення викликів методів)**

Замість того щоб передавати розрахункові значення через параметри до View, ViewModel надає властивості (AveragePrice, TotalQuantity) напряму. 


  

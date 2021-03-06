## Исключение цепных правил

### Определения
**Контекстно-свободная грамматика** – формальная грамматика, у которой в левых частях всех правил стоят только одиночные нетерминалы. Формальную грамматику принятно записывать в виде объединения четрыёх множеств:
1. *V<sub>T</sub>* – множество терминалов.
2. *V<sub>N</sub>* – множество нетерминалов.
3. *P* – множество правил вывода.
4. *S* – начальный символ грамматики.

**Цепное правило** – правило вида *A → B*, где *A* и *B* являются нетерминалами.

### Алгоритм исключения цепных правил
1. Найти все цепные пары в грамматике *Γ*.
2. Для каждой цепной пары (*A*, *B*) добавить в грамматику *Γ′* все правила вида *A → α*, где *B → α* – нецепное правило из *Γ*.
3. Удалить все цепные правила.

Найти все цепные пары можно по индукции:
1. Базис. (*A*, *A*) – цепная пара для любого нетерминала.
2. Индукция. Если пара (*A*, *B*) – цепная, и есть правило *B → C*, то (*A*, *C*) – цепная пара.

### Особенности реализации
**Входные данные:** файл формата '.txt' с междустрочным разделителем '\n' (на первой строке через запятую записаны терминалы, на второй – нетерминалы, на последующих – правила вывода, *S* – начальный символ).

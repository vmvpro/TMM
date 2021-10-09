Public Module LibraryVB



    'Attribute VB_Name = "ksConst"
    '   Ошибки!!!
    '   result >-1  успешное завершение или не фатальные ошибки
    '   result <0   фатальные ошибки - работа прекращается


    Public Const MAXERROR = 205


    ' типы документов
    Public Const lt_DocSheetStandart = 1        ' тип документа 1- чертеж стандартный
    Public Const lt_DocSheetUser = 2            '               2- чертеж нестандартный
    Public Const lt_DocFragment = 3             '               3- фрагмент
    Public Const lt_DocSpc = 4                  '               4- спецификация
    Public Const lt_DocPart3D = 5               '               5- 3d-документ модель
    Public Const lt_DocAssemble3D = 6           '               6- 3d-документ сборка
    Public Const lt_DocTxtStandart = 7          '               7- текстовый документ стандартный
    Public Const lt_DocTxtUser = 8              '               8- текстовый документ нестандартный
    Public Const lt_DocSpcUser = 9              '               9- спецификация нестандартный формат

    ' типы точек привязки текста
    Public Const txta_Left = 0   ' точка привязки слева
    Public Const txta_Center = 1 ' точка привязки вцентре
    Public Const txta_Right = 2  ' точка привязки справа

    ' система квалитета
    Public Const lt_qsShaft = 1    ' 1 - вала
    Public Const lt_qsHole = 2     ' 2 - отверстия

    ' квалитеты
    Public Const lt_qdPreferable = 1  ' 1 - предпочтительные
    Public Const lt_qdBasic = 2       ' 2 - основные
    Public Const lt_qdAdditional = 3  ' 3 - дополнительные

    ' типы данных для LtVariant
    Public Const ltv_Char = 1    ' 1 - символ
    Public Const ltv_UChar = 2   ' 2 - байт
    Public Const ltv_Int = 3     ' 3 - целое
    Public Const ltv_UInt = 4    ' 4 - беззнаковое целое
    Public Const ltv_Long = 5    ' 5 - длинное целое
    Public Const ltv_Float = 6   ' 6 - вешественное
    Public Const ltv_Double = 7  ' 7 - двойное вешественное
    Public Const ltv_Str = 8     ' 8 - строка 255 символов
    Public Const ltv_NoUsed = 9  ' 9 - пока не используется
    Public Const ltv_Short = 10  ' 10 - короткое целое
    Public Const ltv_WStr = 11   ' 11 - строка 255 символов Unicode


    Public Const TEXT_LENGTH = 128
    Public Const MAX_TEXT_LENGTH = 255

    Public Const ODBC_DB = 0
    Public Const TXT_DB = 1

    Public Const TXT_CHAR = 1
    Public Const TXT_USHORT = 2
    Public Const TXT_SSHORT = 3
    Public Const TXT_SLONG = 4
    Public Const TXT_ULONG = 5
    Public Const TXT_LONG = 6
    Public Const TXT_FLOAT = 7
    Public Const TXT_DOUBLE = 8
    Public Const TXT_INT = 9
    Public Const TXT_ALL = 0
    Public Const TXT_INDEX = "Index1000"

    Public Const stACTIVE = 0     ' состояние для вида, слоя, документа
    Public Const stREADONLY = 1   ' состояние для вида, слоя
    Public Const stINVISIBLE = 2  ' состояние для вида, слоя
    Public Const stCURRENT = 3    ' состояние для вида, слоя
    Public Const stPASSIVE = 1    ' состояние для документа


    ' Определения для функции ksSytemPath
    Public Const sptSYSTEM_FILES = 0           ' Выдать путь на каталог системных файлов
    Public Const sptLIBS_FILES = 1             ' Выдать путь на каталог файлов библиотек
    Public Const sptTEMP_FILES = 2             ' Выдать путь на каталог сохранения временных файлов
    Public Const sptCONFIG_FILES = 3           ' Выдать путь на каталог сохранения конфигурации системы
    Public Const sptINI_FILE = 4               ' Выдать полное имя INI-файла системы
    Public Const sptBIN_FILE = 5               ' Выдать путь на каталог исполняемых файлов системы
    Public Const sptPROJECT_FILES = 6          ' Выдать путь на каталог сохранения kompas.prj
    Public Const sptDESKTOP_FILES = 7          ' Выдать путь на каталог сохранения kompas.dsk
    Public Const sptTEMPLATES_FILES = 8        ' Выдать путь на каталог шаблонов Компас-документов
    Public Const sptPROFILES_FILES = 9         ' Выдать путь на каталог сохранения профилей пользователя
    Public Const sptWORK_FILES = 10            ' Выдать путь на каталог "Мои документы"
    Public Const sptSHEETMETAL_FILES = 11      ' Выдать путь на каталог таблиц сгибов
    Public Const sptPARTLIB_FILES = 12         ' Выдать путь на каталог PartLib
    Public Const sptMULTILINE_FILES = 13       ' Выдать путь к каталогу шаблонов мультилинии
    Public Const sptPRINTDEVICE_FILES = 14     ' Выдать путь к каталогу конфигураций плоттеров/принтеров
    Public Const sptCURR_WORK_FILES = 15       ' запоминание последних директориев, с которых выполнилось открытие/сохранение файла в диалоге Open|Save
    Public Const sptCURR_LIBS_FILES = 16       ' запоминание последних директориев, с которых выполнилось открытие/сохранение файла в диалоге Open|Save
    Public Const sptCURR_SYSTEM_FILES = 17     ' запоминание последних директориев, с которых выполнилось открытие/сохранение файла в диалоге Open|Save
    Public Const sptCURR_PROFILES_FILES = 18   ' запоминание последних директориев, с которых выполнилось открытие/сохранение файла в диалоге Open|Save
    Public Const sptCURR_SHEETMETAL_FILES = 19 ' запоминание последних директориев, с которых выполнилось открытие/сохранение файла в диалоге Open|Save

    ' Определения для результата функции SystemControlStart
    Public Const scsSTOPPED_FOR_MENU_COMMAND = 1       ' Выполнена команда меню "Остановить работу библиотеки"
    Public Const scsSTOPPED_FOR_SYSTEM_STOP = 0        ' Идет закрытие задачи
    Public Const scsSTOPPED_FOR_ITSELF = -1            ' Вызов функции SystemControlStop из-под библиотеки
    Public Const scsSTOPPED_FOR_START_THIS_LIB = -2    ' Принудительный останов при запуске той же библиотеки
    Public Const scsSTOPPED_FOR_START_ANOTHER_LIB = -3 ' Принудительный останов при запуске другой библиотеки

    '  Определения для функций GetObjParam и SetObjParam
    '  '+'  отмечены объекты, для которых реализованы  GetObjParam и SetObjParam
    '  '+-'  отмечены объекты, для которых реализован только GetObjParam
    Public Const ALLPARAM = -1                 ' все параметры объекта в системе координат владельца
    Public Const SHEET_ALLPARAM = -2           ' тоже что и  ALLPARAM  но параметры объекта в СК листа
    Public Const NURBS_CLAMPED_PARAM = -5      ' параметры нурбса, преобразовать узловой вектор в зажатый
    Public Const NURBS_CLAMPED_SHEETPARAM = -6 ' параметры нурбса в СК листа, преобразовать узловой вектор в зажатый
    Public Const VIEW_ALLPARAM = -7            ' все параметры объекта в СК вида

    Public Const ANGLE_ARC_PARAM = 0          ' параметры дуги по углам ( для дуги и эллиптической дуги ) в СК объекта владельца
    Public Const POINT_ARC_PARAM = 1          ' параметры дуги по точкам ( для дуги и эллиптической дуги ) в СК объекта владельца
    Public Const ANGLE_ARC_SHEET_PARAM = 2    ' параметры дуги по углам ( для дуги и эллиптической дуги ) в СК листа
    Public Const POINT_ARC_SHEET_PARAM = 3    ' параметры дуги по точкам ( для дуги и эллиптической дуги ) в СК листа
    Public Const ANGLE_ARC_VIEW_PARAM = 4     ' параметры дуги по углам ( для дуги и эллиптической дуги ) в СК вида
    Public Const POINT_ARC_VIEW_PARAM = 5     ' параметры дуги по точкам ( для дуги и эллиптической дуги ) в СК вида

    Public Const VIEW_LAYER_STATE = 1         ' состояние слоя ,вида
    Public Const DOCUMENT_STATE = 1           ' состояние документа
    Public Const DOCUMENT_SIZE = 0            ' размер листа
    Public Const DIM_TEXT_PARAM = 0           ' параметры текста для размеров
    Public Const DIM_SOURSE_PARAM = 1         ' параметры привязки размера
    Public Const DIM_DRAW_PARAM = 2           ' параметры отрисовки размера
    Public Const DIM_VALUE = 3                ' значение размера - double
    Public Const DIM_PARTS = 4                ' составляющие части для размеров struct DimensionPartsParam
    Public Const SHEET_DIM_PARTS = 5          ' составляющие части для размеров struct DimensionPartsParam в СК листа
    Public Const TECHNICAL_DEMAND_PAR = -1    ' параметры технических требований -
    Public Const TT_FIRST_STR = 1000          ' начало отсчета для получения или замены текста ТТ по строкам
    Public Const CONIC_PARAM = 2              ' параметры для построения конического сечения ( для эллипса и эллтптической дуги )
    Public Const SPC_TUNING_PARAM = 0         ' параметры настроек для стиля СП
    Public Const HATCH_PARAM_EX = 0           ' параметры штриховки расширенные
    Public Const ASSOCIATION_VIEW_PARAM = 0   ' параметры ассоциативного вида
    Public Const DIM_SOURSE_VIEWPARAM = 7     ' параметры привязки размера в ситеме координат вида
    Public Const DIM_DRAW_VIEWPARAM = 8       ' параметры отрисовки размера в ситеме координат вида
    Public Const DIM_SOURSE_SHEETPARAM = 9    ' параметры привязки размера в ситеме координат листа
    Public Const DIM_DRAW_SHEETPARAM = 10     ' параметры отрисовки размера в ситеме координат листа


    Public Const ALL_OBJ = 0                  ' все объекты,кроме вспомогательных, которые могут входить в вид                    -
    Public Const LINESEG_OBJ = 1              ' отрезок                        +
    Public Const CIRCLE_OBJ = 2               ' окружность                     +
    Public Const ARC_OBJ = 3                  ' дуга                           +
    Public Const TEXT_OBJ = 4                 ' текст                          +
    Public Const POINT_OBJ = 5                ' точка                          +
    Public Const HATCH_OBJ = 7                ' штриховка                      +
    Public Const BEZIER_OBJ = 8               ' bezier сплайн                  +
    Public Const LDIMENSION_OBJ = 9           ' линейный размер                +
    Public Const ADIMENSION_OBJ = 10          ' угловой размер                 +
    Public Const DDIMENSION_OBJ = 13          ' диаметральный размер           +
    Public Const RDIMENSION_OBJ = 14          ' радиальный размер              +
    Public Const RBREAKDIMENSION_OBJ = 15     ' радиальный размер с изломом    +
    Public Const ROUGH_OBJ = 16               ' шероховатость                  +
    Public Const BASE_OBJ = 17                ' база                           +
    Public Const WPOINTER_OBJ = 18            ' стрелка вида                   +
    Public Const CUT_OBJ = 19                 ' линия разреза                  +
    Public Const LEADER_OBJ = 20              ' простая линия выноски                      +
    Public Const POSLEADER_OBJ = 21           ' линия выноски для обозначения позиции      +
    Public Const BRANDLEADER_OBJ = 22         ' линия выноски для обозначения клеймения    +
    Public Const MARKERLEADER_OBJ = 23        ' линия выноски для обозначения маркирования +
    Public Const TOLERANCE_OBJ = 24           ' допуск формы                   +
    Public Const TABLE_OBJ = 25               ' таблица                        +     //тексты
    Public Const CONTOUR_OBJ = 26             ' контур                         +     //стиль
    Public Const MACRO_OBJ = 27               ' нетипизированный макроэлемент  -
    Public Const LINE_OBJ = 28                ' линия                          +
    Public Const LAYER_OBJ = 29               ' слой                           +
    Public Const FRAGMENT_OBJ = 30            ' вставной фрагмент              +
    Public Const POLYLINE_OBJ = 31            ' полилиния                      +
    Public Const ELLIPSE_OBJ = 32             ' эллипс                         +
    Public Const NURBS_OBJ = 33               ' nurbs сплайн                   +
    Public Const ELLIPSE_ARC_OBJ = 34         ' дуга эллипса                   +
    Public Const RECTANGLE_OBJ = 35           ' прямоугольник                  +
    Public Const REGULARPOLYGON_OBJ = 36      ' многоугольник                  +
    Public Const EQUID_OBJ = 37               ' эквидистанта                   +
    Public Const LBREAKDIMENSION_OBJ = 38     ' линейный размер с обрывом      +
    Public Const ABREAKDIMENSION_OBJ = 39     ' угловой размер с обрывом       +
    Public Const ORDINATEDIMENSION_OBJ = 40   ' размер высоты
    Public Const COLORFILL_OBJ = 41           ' фоновая заливка цветом         +
    Public Const CENTREMARKER_OBJ = 42        ' обозначение центра             +
    Public Const ARCDIMENSION_OBJ = 43        ' размер длины дуги
    Public Const SPC_OBJ = 44                 ' объект спецификации            +
    Public Const RASTER_OBJ = 45              ' растровый объект               +
    Public Const CHANGE_LEADER_OBJ = 46       ' Обозначение изменения          +
    Public Const REMOTE_ELEMENT_OBJ = 47      ' выносной элемент               -
    Public Const AXISLINE_OBJ = 48            ' Осевая линия                   +
    Public Const OLEOBJECT_OBJ = 49           ' Вставка ole объекта            -
    Public Const KNOTNUMBER_OBJ = 50          ' объект номер узла              -
    Public Const BRACE_OBJ = 51               ' объект фигурная скобка         -
    Public Const POSNUM_OBJ = 52              ' марка/позиционное обозначение с линией-выноской -
    Public Const MARKONLDR_OBJ = 53           ' марка/позиционное обозначение на линии          -
    Public Const MARKWOLDR_OBJ = 54           ' марка/позиционное обозначение без линии-выноски -
    Public Const WAVELINE_OBJ = 55            ' волнистая линия                -
    Public Const DIRAXIS_OBJ = 56             ' прямая ось                     -
    Public Const BROKENLINE_OBJ = 57          ' линия обрыва с изломами        -
    Public Const CIRCLEAXIS_OBJ = 58          ' круговая ось                   -
    Public Const ARCAXIS_OBJ = 59             ' дуговая ось                    -
    Public Const CUTUNITMARKING = 60          ' Обозначение узла в сечении     -
    Public Const UNITMARKING = 61             ' Обозначение узла      -
    Public Const MULTITEXTLEADER = 62         ' Выносная надпись к многослойным конструкциям.      -
    Public Const EXTERNALVIEW_OBJ = 63        ' Вставка внешнего вида                          -
    Public Const ANNLINESEG_OBJ = 64          ' Аннотационный отрезок                 +- Для GetObjParam используется структура LineSegParam
    Public Const ANNCIRCLE_OBJ = 65           ' Аннотационная окружность              +- Для GetObjParam используется структура CircleParam
    Public Const ANNELLIPSE_OBJ = 66          ' Аннотационный эллипс                  +- Для GetObjParam используется структура EllipseParam
    Public Const ANNARC_OBJ = 67              ' Аннотационная дуга                    +- Для GetObjParam используется структура ArcParam
    Public Const ANNELLIPSE_ARC_OBJ = 68      ' Аннотационная дуга эллипса            +- Для GetObjParam используется структура EllipseArcParam
    Public Const ANNPOLYLINE_OBJ = 69         ' Аннотационная полилиния               +- Для GetObjParam используется структура PolylineParam
    Public Const ANNPOINT_OBJ = 70            ' Аннотационная точка                   +- Для GetObjParam используется структура PointParam
    Public Const ANNTEXT_OBJ = 71             ' Текст с аннотационной точкой привязки +- Для GetObjParam используется структура TextParam
    Public Const MULTILINE_OBJ = 72           ' Мультилиния                    -
    Public Const BUILDINGCUTLINE_OBJ = 73     ' Линия разреза/сечения для СПДС + используется структура CutLineParam
    Public Const ATTACHED_LEADER_OBJ = 74     ' Присоединенная линия выноски ( не имеет текстов )  +

    Public Const MAX_VIEWTIP_SEARCH = 75      ' Верхняя граница типов поиска для объектов вида  -

    Public Const SPECIFICATION_OBJ = 121      ' спецификация на листе
    Public Const SPECROUGH_OBJ = 122          ' неуказанная шероховатость      +
    Public Const VIEW_OBJ = 123               ' вид                            +
    Public Const DOCUMENT_OBJ = 124           ' документ  графический          +   (лист или фрагмент)
    Public Const TECHNICALDEMAND_OBJ = 125    ' технические требования         +
    Public Const STAMP_OBJ = 126              ' штамп                          +  //тексты
    Public Const SELECT_GROUP_OBJ = 127       ' группа селектирования          -
    Public Const NAME_GROUP_OBJ = 128         ' именная группа                 -
    Public Const WORK_GROUP_OBJ = 129         ' рабочая группа                 -
    Public Const SPC_DOCUMENT_OBJ = 130       ' документ  спецификация         +
    Public Const D3_DOCUMENT_OBJ = 131        ' 3d документ  модели или сборки +
    Public Const CHANGE_LIST_OBJ = 132        ' таблица изменений              -
    Public Const TXT_DOCUMENT_OBJ = 133       ' текстовый документ
    Public Const ALL_DOCUMENTS = 134          ' документы всех типов

    Public Const MAX_TIP_SEARCH = 134         ' верхняя граница типов поиска   -
    Public Const ALL_OBJ_SHOW_ORDER = -1000   ' все объекты которые могут входить в вид в порядке отрисовки



    ' Поле тип линии имеет значения( системные стили ) :
    ' 1  - основная,
    ' 2  - тонкая,
    ' 3  - осевая,
    ' 4  - штриховая,
    ' 5  - волнистая линия
    ' 6  - вспомогательная,
    ' 7  - утолщенная,
    ' 8  - штрих-пунктир с 2 точками,
    ' 9  - штриховая толстая
    ' 10 -осевая толстая
    ' 11 -тонкая, включаемая в штриховку
    ' 12 - ISO штриховая линия
    ' 13 - ISO штриховая линия (дл. пробел)
    ' 14 - ISO штрихпунктирная линия (дл. штрих)
    ' 15 - ISO штрихпунктирная линия (дл. штрих 2 пунктира)
    ' 16 - ISO штрихпунктирная линия (дл. штрих 3 пунктира)
    ' 17 - ISO пунктирная линия
    ' 18 - ISO штрихпунктирная линия (дл. и кор. штрихи)
    ' 19 - ISO штрихпунктирная линия (дл. и 2 кор. штриха)
    ' 20 - ISO штрихпунктирная линия
    ' 21 - ISO штрихпунктирная линия (2 штриха)
    ' 22 - ISO штрихпунктирная линия (2 пунктира)
    ' 23 - ISO штрихпунктирная линия (3пунктира)
    ' 24 - ISO штрихпунктирная линия (2 штриха 2 пунктира)
    ' 25 - ISO штрихпунктирная линия (2 штриха 3 пунктира)


    ' Поле тип точки для точки( системные стили ) :
    ' 0 - точка
    ' 1 - крестик
    ' 2 - х-точка
    ' 3 - квадрат
    ' 4 - треугольник
    ' 5 - окружность
    ' 6 - звезда
    ' 7 - перечеркнутый квадрат
    ' 8 - утолщенный плюс

    ' Поле тип штриховки для штриховки( системные стили ) :
    ' 0  - металл
    ' 1  - неметалл
    ' 2  - дерево
    ' 3  - камень естественный
    ' 4  - керамика
    ' 5  - бетон
    ' 6  - стекло
    ' 7  - жидкость
    ' 8  - естественный грунт
    ' 9  - насыпной грунт
    ' 10 - камень искусственный
    ' 11 - железобетон
    ' 12 - напряженный железобетон
    ' 13 - дерево в продольном сечении
    ' 14 - песок

    ' Определения флагов для текста
    Public Const INVARIABLE = 0              ' не менять флаги текста

    Public Const NUMERATOR = &H1             ' числитель
    Public Const DENOMINATOR = &H2           ' знаменатель
    Public Const END_FRACTION = &H3          ' конец дроби
    Public Const UPPER_DEVIAT = &H4          ' верхнее отклонение
    Public Const LOWER_DEVIAT = &H5          ' нижнее отклонение
    Public Const END_DEVIAT = &H6            ' конец  отклонений
    Public Const S_BASE = &H7                ' основание выражения типа суммы
    Public Const S_UPPER_INDEX = &H8         ' верхний индекс выражения типа суммы
    Public Const S_LOWER_INDEX = &H9         ' нижний индекс выражения типа суммы
    Public Const S_END = &H10                ' конец выражения типа суммы
    Public Const SPECIAL_SYMBOL = &H11       ' спецзнак
    Public Const SPECIAL_SYMBOL_END = &H12   ' для спецзнаков с текстом
    Public Const RETURN_BEGIN = &H13         ' начало для ввода следующих строк в спецзнаке с текстом, дробях, отклонениях
    Public Const RETURN_DOWN = &H14          ' для ввода следующих строк в спецзнаке с текстом, дробях, отклонениях
    Public Const RETURN_RIGHT = &H15         ' для ввода строк справа в спецзнаке с текстом, дробях, отклонениях
    Public Const TAB_ = &H16                 ' табуляция по текущему стилю
    Public Const FONT_SYMBOL = &H17          ' символ фонта
    Public Const FONT_SYMBOL_W = &O2017      ' символ фонта Unicode
    Public Const HYPER_TEXT = &O2000         ' ссылка на текст или положение объекта

    Public Const ITALIC_ON = &H40        ' включить наклон
    Public Const ITALIC_OFF = &H80       ' выключть наклон
    Public Const BOLD_ON = &H100         ' включить толщину
    Public Const BOLD_OFF = &H200        ' выключть толщину
    Public Const UNDERLINE_ON = &H400    ' включить подчеркивание
    Public Const UNDERLINE_OFF = &H800   ' выключть подчеркивание
    Public Const NEW_LINE = &H1000       ' новая строка в параграфе

    Public Const FONT_NAME = 1               '  имя фонта
    Public Const NARROWIN = 2               '  коэффициент сужения фонта "была переменная NARROWING"
    Public Const HEIGHT = 3                  '  высота фонта
    Public Const COLOR = 4                   '  цвет текста
    Public Const SPECIAL = 5                 '  спецзнак
    Public Const FRACTION_TYPE = 6           '  высота дроби по отношению к тексту 1-полная высота 2-в 1.5 раза меньше 3-в 2 раза меньше
    Public Const SUM_TYPE = 7                '  высота выражения типа суммы по отношению к тексту 1-полная высота 2-в 1.5 раза больше

    ' Поле style для текста( системные стили ) :
    ' 0 -умолчательный стиль для данного типа объекта
    ' 1  текст на чертеже
    ' 2  текст для технических требований
    ' 3  текст размерных надписей
    ' 4  текст шероховатости
    ' 5  текст для линии выноски  ( позиционной )
    ' 6  текст для линии выноски  ( над\под полкой )
    ' 7  текст для линии выноски  ( сбоку )
    ' 8  текст для допуска формы
    ' 9  текст для таблицы ( заголовок )
    ' 10 текст для таблицы ( ячейка )
    ' 11 текст для линии разреза
    ' 12 текст для стрелки вида
    ' 13 текст для для неуказанной шероховатости
    ' 14 текст для обозначения изменения
    ' 15 текст для фигурной скобки
    ' 16 текст для номера узла
    ' 17 текст для выносной надписи
    ' 18 текст для обозначения узла
    ' 19 текст для марки координационной оси
    ' 20 текст для МПО(марка/позиционное обозначение с линией-выноской)
    ' 21 текст для МПО(марка/позиционное обозначение) на линии
    ' 22 текст для МПО(марка/позиционное обозначение) без линии выноски
    ' 23 текст для заголовков спецификации


    ' ***************************************************************/
    ' * Структуры для работы с табличными атрибутами  */
    ' ***************************************************************/
    Public Const CHAR_ATTR_TYPE = 1
    Public Const UCHAR_ATTR_TYPE = 2
    Public Const INT_ATTR_TYPE = 3
    Public Const UINT_ATTR_TYPE = 4
    Public Const LINT_ATTR_TYPE = 5
    Public Const FLOAT_ATTR_TYPE = 6
    Public Const DOUBLE_ATTR_TYPE = 7
    Public Const STRING_ATTR_TYPE = 8    ' строка фиксированной длины MAX_TEXT_LENGTH
    Public Const RECORD_ATTR_TYPE = 9

    ' Определения для линейного размера
    Public Const AUTONOMINAL = &H1         ' >0 простановка размера автоматическая
    Public Const RECTTEXT = &H2            ' >0 текст в рамочке
    Public Const PREFIX = &H4              ' >0 есть текст до номинала
    Public Const NOMINALOFF = &H8          ' >0 нет  номинала
    Public Const TOLERANCE = &H10          ' >0 квалитет проставлять
    Public Const DEVIATION = &H20          ' >0 отклонения проставлять
    Public Const UNIT = &H40               ' >0 единица измерения
    Public Const SUFFIX = &H80             ' >0 есть текст после номинала
    Public Const DEVIATION_INFORM = &H100  ' >0 при включенном _DEVIATION, отклонения есть в массиве текстов( даже если не ручное проставление отклонений).
    '    Появляется после  функции GetObjParam, чтобы пользователь мог получить отклонения.
    Public Const UNDER_LINE = &H200        ' >0 размер с подчеркиванием
    Public Const BRACKETS = &H400          ' >0 размер в скобках
    Public Const SQUARE_BRACKETS = &H800   ' >0 размер в квадратных скобках, используется вместе с _BRACKETS
    '  BRACKETS                    - размер в круглых скобках
    '  BRACKETS | _SQUARE_BRACKETS - размер в квадратных скобках

    Public Const INDICATIN_TEXT_LINE_ARR = &HFFFF    ' для шероховаиости, позиционной линии выноски, маркировки и клеймения
    ' признак, что для текста используется динамический массив TEXT_LINE_ARR

    ' типы стилей
    Public Const CURVE_STYLE = 1     ' стиль криивых
    Public Const HATCH_STYLE = 2     ' стиль штриховок
    Public Const TEXT_STYLE = 3      ' стиль текста
    Public Const STAMP_STYLE = 4     ' стиль штампа
    Public Const CURVE_STYLE_EX = 5  ' стиль криивых расширенный

    ' curveType
    Public Const LIKE_BASIC_LINE = &H10 ' параметры пера как у  основной линии
    Public Const LIKE_THIN_LINE = &H20  ' параметры пера как у  тонкой линии
    Public Const LIKE_HEAVY_LINE = &H30 ' параметры пера как у  утолщенной линии

    ' Определения для функций GetSysOptions и  SetSysOptions
    Public Const DIMENTION_OPTIONS = 1          ' настройки размера
    Public Const SNAP_OPTIONS = 1               ' настройки привязок
    Public Const ARROWFILLING_OPTIONS = 2       ' Зачернять стрелки ?
    Public Const SHEET_OPTIONS = 3              ' Параметры листа
    Public Const SHEET_OPTIONS_EX = 4           ' Параметры листа документа
    Public Const LENGTHUNITS_OPTIONS = 5        ' Настройки единиц измерений
    Public Const SNAP_OPTIONS_EX = 6            ' Настройки привязок документа
    Public Const VIEWCOLOR_OPTIONS = 7          ' Настройки цвета фона рабочего поля 2d - документов
    Public Const TEXTEDIT_VIEWCOLOR_OPTIONS = 8 ' Настройки цвета фона редактирования текста
    Public Const MODEL_VIEWCOLOR_OPTIONS = 9    ' Настройки цвета фона для моделей
    Public Const OVERLAP_OBJECT_OPTIONS = 10    ' Настройки перекрывающихся объектов
    Public Const DIMENTION_OPTIONS_EX = 11      ' Настройки размера

    ' типы колонок для спецификации
    Public Const SPC_CLM_FORMAT = 1     '  формат
    Public Const SPC_CLM_ZONE = 2       '  зона
    Public Const SPC_CLM_POS = 3        '  позиция
    Public Const SPC_CLM_MARK = 4       '  обозначение
    Public Const SPC_CLM_NAME = 5       '  наименование
    Public Const SPC_CLM_COUNT = 6      '  количество
    Public Const SPC_CLM_NOTE = 7       '  примечание
    Public Const SPC_CLM_MASSA = 8      '  масса
    Public Const SPC_CLM_MATERIAL = 9   '  материал
    Public Const SPC_CLM_USER = 10      '  пользовательская
    Public Const SPC_CLM_KOD = 11       '  код
    Public Const SPC_CLM_FACTORY = 12   '  завод изготовитель

    ' типы значений для колонки спецификации
    Public Const SPC_INT = 1        ' целый
    Public Const SPC_DOUBLE = 2     ' вещественный
    Public Const SPC_STRING = 3     ' строка
    Public Const SPC_RECORD = 4     ' запись

    ' типы блиотек стилей
    Public Const CURVE_STYLE_LIBRARY = 1              ' библиотека стилей кривых (*.lcs)
    Public Const HATCH_STYLE_LIBRARY = 2              ' библиотека стилей штриховок (*.lhs)
    Public Const TEXT_STYLE_LIBRARY = 3               ' библиотека стилей текстов   (*.lts)
    Public Const STAMP_LAYOUT_STYLE_LIBRARY = 4       ' библиотека стилей описаний штампов (*.lyt)
    Public Const GRAPHIC_LAYOUT_STYLE_LIBRARY = 5     ' библиотека стилей оформлений графических документов (*.lyt)
    Public Const TEXT_LAYOUT_STYLE_LIBRARY = 6        ' библиотека стилей оформлений текстовых документов (*.lyt)
    Public Const SPC_LAYOUT_STYLE_LIBRARY = 7         ' библиотека стилей оформлений спецификаций (*.lyt)

    ' размерности и типы детали для рассчета массо-ценровочных характеристик
    Public Const ST_MIX_MM = &H1       ' миллиметры
    Public Const ST_MIX_SM = 0         ' сантиметры
    Public Const ST_MIX_DM = &H2       ' дециметры
    Public Const ST_MIX_M = &H3        ' метры
    Public Const ST_MIX_GR = 0         ' граммы
    Public Const ST_MIX_KG = &H10      ' килограммы
    Public Const ST_MIX_EXT = 0        ' выдавливание
    Public Const ST_MIX_RV = &H20      ' вращение

    ' тип локальной привязки
    Public Const SN_NEAREST_POINT = 1     ' Ближайшая точка
    Public Const SN_NEAREST_MIDDLE = 2    ' Середина
    Public Const SN_CENTRE = 3            ' Центр
    Public Const SN_INTERSECT = 4         ' Пересечение
    Public Const SN_GRID = 5              ' По сетке
    Public Const SN_XY_ALIGN = 6          ' Выравнивание
    Public Const SN_ANGLE = 7             ' Угловая привязка
    Public Const SN_POINT_CURVE = 8       ' Точка на кривой

    ' типы общих настроек для привязок
    Public Const SN_DYNAMICALLY = &H1       ' привязки отслеживать динамически
    Public Const SN_ASSISTANT = &H2         ' писать текст
    Public Const SN_BACKGROUND_LAYER = &H4  ' учитывать фоновые слои и виды
    Public Const SN_SUSPENDED = &H8         ' подавить привязки


    ' Типы параметрических ограничений
    Public Const CONSTRAINT_FIXED_POINT = 1            ' фиксировать точку
    Public Const CONSTRAINT_POINT_ON_CURVE = 2         ' точка на кривой
    Public Const CONSTRAINT_HORIZONTAL = 3             ' горизонталь
    Public Const CONSTRAINT_VERTICAL = 4               ' вертикаль
    Public Const CONSTRAINT_PARALLEL = 5               ' параллельность двух прямых или отрезков
    Public Const CONSTRAINT_PERPENDICULAR = 6          ' перпендикулярность двух прямых или отрезков
    Public Const CONSTRAINT_EQUAL_LENGTH = 7           ' равенство длин двух отрезков
    Public Const CONSTRAINT_EQUAL_RADIUS = 8           ' равенство радиусов двух дуг/окружностей
    Public Const CONSTRAINT_HOR_ALIGN_POINTS = 9       ' выравнивать две точки по горизонтали
    Public Const CONSTRAINT_VER_ALIGN_POINTS = 10      ' выравнивать две точки по вертикали
    Public Const CONSTRAINT_MERGE_POINTS = 11          ' совпадение двух точек
    Public Const CONSTRAINT_TANGENT_TWO_CURVES = 15    ' касание двух кривых
    Public Const CONSTRAINT_SYMMETRY_TWO_POINTS = 16   ' симетрия двух точек
    Public Const CONSTRAINT_COLLINEAR = 17             ' колиниарность отрезков
    Public Const CONSTRAINT_FIXED_ANGLE = 18           ' фиксированный угол
    Public Const CONSTRAINT_FIXED_LENGHT = 19          ' фиксированная длина
    Public Const CONSTRAINT_POINT_ON_CURVE_MIDDLE = 20 ' точка на серидине кривой
    Public Const CONSTRAINT_BISECTOR = 21              ' биссектриса


    ' типы объектов спецификации
    Public Const SPC_BASE_OBJECT = 1       ' базовый объект (редактируется пользователем)
    Public Const SPC_COMMENT = 2           ' комментарий    (редактируется пользователем)
    Public Const SPC_SECTION_NAME = 3      ' наименование раздела ( создается по стилю СП автоматически )
    Public Const SPC_BLOCK_NAME = 4        ' наименование блока исполнений ( создается по стилю СП автоматически )
    Public Const SPC_RESERVE_STR = 5       ' резервная строка ( создается по стилю СП автоматически )
    Public Const SPC_EMPTY_STR = 6         ' пустая строка ( создается по стилю СП автоматически )

    ' типы сортировки
    Public Const SPC_SORT_OFF = 0        ' нет сортировки
    Public Const SPC_SORT_COMPOS = 1     ' составная сортировка
    Public Const SPC_SORT_ALPHABET = 2   ' сортировка по алфавиту
    Public Const SPC_SORT_UP = 3         ' сортировка по возрастанию колонок
    Public Const SPC_SORT_DOCUMENT = 4   ' сортировка раздела документация
    Public Const SPC_SORT_DOWN = 5       ' сортировка по убыванию колонок
    Public Const SPC_SORT_COMPOSDOWN = 6 ' составная сортировка по убыванию

    ' //////////////////////////////////////////////////////////////////////////////
    '
    '  типы специальных символов ( аннотационный объект )
    '
    ' //////////////////////////////////////////////////////////////////////////////
    Public Const ARROW_INSIDE_SYMBOL = 1          ' стрелка изнутри
    Public Const ARROW_OUT_SIDE_SYMBOL = 2        ' стрелка снаружи
    Public Const TICK_TAIL_SYMBOL = 3             ' засечка с продолжением кривой (с хвостиком)
    Public Const UP_HALF_ARROW_SYMBOL = 4         ' верхняя половина стрелки изнутри
    Public Const DOWN_HALF_ARROW_SYMBOL = 5       ' нижняя половина стрелки изнутри
    Public Const BIG_ARROW_INSIDE_SYMBOL = 6      ' большая стрелка изнутри (7мм)
    Public Const ARROW_ORDINATE_DIM_SYMBOL = 7    ' стрелка для размера высоты(штрихи длиной 4 мм под углом 45 гр)
    Public Const TRIANGLE_SYMBOL = 8              ' треугольник по напр-нию кривой
    Public Const CIRCLE_RAD2_SYMBOL = 9           ' окружность радиусом 2 мм тонкой линией - для шер-сти и линии-выноски
    Public Const CENTRE_MARKER_SYMBOL = 10        ' обозначение фиктивного центра в виде большого креста
    Public Const GLUE_SIGN_SYMBOL = 11            ' знак склеивания
    Public Const SOLDER_SIGN_SYMBOL = 12          ' знак пайки
    Public Const SEWING_SIGN_SYMBOL = 13          ' знак сшивания
    Public Const CRAMP_SIGN_SYMBOL = 14           ' знак соединения внахлестку металл.скобами
    Public Const CORNER_CRAMP_SIGN_SYMBOL = 15    ' знак углового соединения металл.скобами
    Public Const MONTAGE_JOINT_SYMBOL = 16        ' знак монтажного шва
    Public Const TICK_SYMBOL = 17                 ' засечка без продолжения кривой (без хвостика)
    Public Const TRIANGLE_CURR_CS = 18            ' треугольник по текущей СК - для базы
    Public Const ARROW_CLOSED_INSIDE = 19         ' закрытая стрелка изнутри
    Public Const ARROW_CLOSED_OUTSIDE = 20        ' закрытая стрелка снаружи
    Public Const ARROW_OPEN_INSIDE = 21           ' открытая стрелка изнутри
    Public Const ARROW_OPEN_OUTSIDE = 22          ' открытая стрелка снаружи
    Public Const ARROW_RIGHTANGLE_INSIDE = 23     ' стрелка 90 град изнутри
    Public Const ARROW_RIGHTANGLE_OUTSIDE = 24    ' стрелка 90 град снаружи
    Public Const SYMBOL_DOT = 25                  ' точка (диаметр равен длины стрелки размера)
    Public Const SYMBOL_SMALLDOT = 26             ' точка маленькая (диаметр равен 0.6 длины стрелки размера)
    Public Const AUXILIARY_POINT = 27             ' вспомогательная точка
    Public Const LEFT_TICK_SYMBOL = 28            ' засечка с наклоном влево

    ' типы предопределенных динамических массивов
    Public Const CHAR_STR_ARR = 1            ' динамический массив указателей на строки символов
    Public Const POINT_ARR = 2               ' динамический массив указателей на математические точки -структура MathPointParam
    Public Const CURVE_PATTERN_ARR = 2       ' динамический массив указателей на участки штриховой линии -структура CurvePattern
    Public Const TEXT_LINE_ARR = 3           ' динамический массив строк текста - структура TextLineParam
    Public Const TEXT_ITEM_ARR = 4           ' динамический массив компонент строк текста структура TextItemParam
    Public Const ATTR_COLUMN_ARR = 5         ' динамический массив колонок аттрибутов- структура  ColumnInfo
    Public Const USER_ARR = 6                ' динамический пользовательский массив
    Public Const POLYLINE_ARR = 7            ' динамический массив полилиний-(указателей массивов POINT_ARR)
    Public Const RECT_ARR = 8                ' динамический массив габаритных прямоугольников-(структура RectParam)
    Public Const LIBRARY_STYLE_ARR = 9       ' динамический массив структур параметров для стиля в библиотеке стилей( LibraryStyleParam )
    Public Const VARIABLE_ARR = 10           ' динамический массив структур параметров параметрических переменных( VariableParam )
    Public Const CURVE_PATTERN_ARR_EX = 11   ' динамический массив указателей на участки штриховой линии -структура CurvePatternEx
    Public Const LIBRARY_ATTR_TYPE_ARR = 12  ' динамический массив структур параметров для типа атрибута в библиотеке типов атрибутов( LibraryAttrTypeParam )
    Public Const NURBS_POINT_ARR = 13        ' динамический массив структур NurbsPointParam
    Public Const DOUBLE_ARR = 14             ' динамический массив duuble
    Public Const CONSTRAIN_ARR = 15          ' динамический массив параметрических ограничений - структура ConstrainParam
    Public Const CORNER_ARR = 16             ' динамический массив структур параметров углов CornerParam для прямоугольников и многоугольников
    Public Const DOC_SPCOBJ_ARR = 17         ' динамический массив структур параметров прикрепленных документов к объекту спецификации
    Public Const SPCSUBSECTION_ARR = 18      ' динамический массив структур параметров подраздела спецификации SpcSubSectionParam
    Public Const SPCTUNINGSEC_ARR = 19       ' динамический массив структур параметров настройки раздела спецификации SpcTuningSectionParam
    Public Const SPCSTYLECOLUMN_ARR = 20     ' динамический массив структур параметров стиля колонки таблицы спецификации SpcStyleColumnParam
    Public Const SPCSTYLESEC_ARR = 21        ' динамический массив структур параметров стиля разделa спецификации SpcStyleSectionParam
    Public Const QUALITYITEM_ARR = 22        ' динамический массив структур QualityItemParam - запись об одном интервале для какого-то квалитета
    Public Const LTVARIANT_ARR = 23          ' динамический массив структур LtVariant
    Public Const TOLERANCEBRANCH_ARR = 24    ' динамический массив структур ToleranceBranch
    Public Const HATCHLINE_ARR = 25          ' динамический массив структур HatchLineParam
    Public Const TREENODEPARAM_ARR = 26      ' динамический массив структур узла дерева TreeNodeParam

    '-----------------------------------------------------------------------------
    ' определения для конвертации в растровый формат
    ' ---
    Public Const FORMAT_BMP = 0
    Public Const FORMAT_GIF = 1
    Public Const FORMAT_JPG = 2
    Public Const FORMAT_PNG = 3
    Public Const FORMAT_TIF = 4
    Public Const FORMAT_TGA = 5
    Public Const FORMAT_PCX = 6
    Public Const FORMAT_WMF = 16
    Public Const FORMAT_EMF = 17

    '-----------------------------------------------------------------------------
    ' определения для настройки цвета растрового формата
    ' ---
    Public Const BLACKWHITE = 0    ' цвет черный
    Public Const COLORVIEW = 1     ' цвет установленный для вида
    Public Const COLORLAYER = 2    ' цвет установленный для слоя
    Public Const COLOROBJECT = 3   ' цвет установленный для объекта

    '-----------------------------------------------------------------------------
    ' орределения бит на пиксел для конвертации в растровый формат
    ' ---
    Public Const BPP_COLOR_01 = 1   ' "Черный"
    Public Const BPP_COLOR_02 = 2   ' "4 цвета"
    Public Const BPP_COLOR_04 = 4   ' "16 цветов"
    Public Const BPP_COLOR_08 = 8   ' "256 цветов"
    Public Const BPP_COLOR_16 = 16  ' "16 разрядов"
    Public Const BPP_COLOR_24 = 24  ' "24 разряда"
    Public Const BPP_COLOR_32 = 32  ' "32 разряда"

    '-----------------------------------------------------------------------------
    ' перечисление возможных типов узла дерева библиотеки документов  LtNodeType
    ' ---
    Public Const tn_root = 0  ' корень дерева
    Public Const tn_dir = 1   ' папка (директория)
    Public Const tn_file = 2  ' документ (файл)

    '------------------------------------------------------------------------------
    ' Типы стандартных видов
    ' ---
    Public Const VIEW_FRONT = &H1  ' Спереди
    Public Const VIEW_REAR = &H2   ' Сзади
    Public Const VIEW_UP = &H4     ' Сверху
    Public Const VIEW_DOWN = &H8   ' Снизу
    Public Const VIEW_LEFT = &H10  ' Слева
    Public Const VIEW_RIGHT = &H20 ' Справа
    Public Const VIEW_ISO = &H40   ' Изометрия

    '------------------------------------------------------------------------------
    ' типы значка объекта "Выносной элемент" LtRemoteElmSignType
    ' ---
    Public Const re_Circle = 0    ' окружность
    Public Const re_Rectangle = 1 ' прямоугольник
    Public Const re_Ballon = 2    ' скругленный прямоугольник

    '------------------------------------------------------------------------------
    ' Тип изменения порядка объектов ChangeOrderType
    ' ---
    Public Const co_Top = 1          ' Выше всех
    Public Const co_Bottom = 2       ' Ниже всех
    Public Const co_BeforeObject = 3 ' Перед объектом
    Public Const co_AfterObject = 4  ' За объектом
    Public Const co_UpLevel = 5      ' На уровень вперед
    Public Const co_DownLevel = 6    ' На уровень назад

    '------------------------------------------------------------------------------
    ' Стандартые курсоры Компас
    ' ---
    Public Const OCR_SELECT = &HFFFE ' Курсор ввиде SELECT
    Public Const OCR_SNAP = &HFFFD   ' Курсор ввиде SNAP
    Public Const OCR_CATCH = &HFFFC  ' Курсор ввиде CATCH
    Public Const OCR_DEDAULT = 0     ' Курсор в виде креста

    '-----------------------------------------------------------------------------
    ' Неопределенный цвет для TextItemFont.color
    ' В стиле может быть выставлен по умолчанию цвет отличный он 0
    ' В этом случае если TextItemFont.color будет значение 0 то создастся
    ' модификатор на цвет и он не будет отображаться как цвет по умолчанию
    ' для того чтобы модификатор цвета не создался нужно
    ' или прислать цвет из настроек или константу FREE_COLOR
    ' ---
    Public Const FREE_COLOR = &HFF000000      '  Неопределенный цвет


End Module
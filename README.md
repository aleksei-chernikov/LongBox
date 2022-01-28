# LongBox
Проект для всякой всячины.

Что интересного:
1. Otreya.Common.CQRS - попытка в свой CQRS без MediatR.
   1. Работает на базе цепочки ответственности [PipelineNet](https://github.com/ipvalverde/PipelineNet);
   2. Реализация только для Microsoft.Extensions.DependencyInjection;
   3. Отдельный IVoidCommand с соответствующим Handler без возвращаемого значения;
   4. Автоматическая регистрация Handler для Command/Query/VoidCommand;
   5. Не поддерживает синхронные вызовы;
   
2. LongBox.Telegram.* - в будущем отдельный сервис для телеграм-бота, а пока крутится в монолите;
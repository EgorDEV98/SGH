using SGH.Data.Enums;

namespace SGH.Application.Models.Responces;

public class GetDeviceResponse
{
    /// <summary>
    /// Идентификатор устройства
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя устройства
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Датчики устройства
    /// </summary>
    public IReadOnlyCollection<GetSensorResponse> Sensors { get; set; }
    
    /// <summary>
    /// Актуаторы
    /// </summary>
    public IReadOnlyCollection<GetActuatorResponse> Actuators { get; set; }
    
    /// <summary>
    /// Дата добавления устройства
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime LastUpdated { get; set; }

    public class GetSensorResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    
        /// <summary>
        /// Имя датчика
        /// </summary>
        public string Name { get; set; }
    
        /// <summary>
        /// Системное имя датчика
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Дата добавления датчика
        /// </summary>
        public DateTime CreatedDate { get; set; }
    
        /// <summary>
        /// Дата последнего обновления
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
    public class GetActuatorResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    
        /// <summary>
        /// Имя актуатора
        /// </summary>
        public string Name { get; set; }
    
        /// <summary>
        /// Системное имя актуатора
        /// </summary>
        public string SystemName { get; set; }
    
        /// <summary>
        /// Текущее состояние
        /// </summary>
        public ActuatorState State { get; set; }
    }
}
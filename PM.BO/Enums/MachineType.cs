namespace PM.BO.Enums
{
    public enum MachineType
    {
        StaticD = 0, /**< Model D, static type (0). */
        StaticC = 1, /**< Model C, static type (1). */
        StaticA = 2, /**< Model A, static type (2). */
        StaticB = 3, /**< Model B, static type (3). */
        StaticE = 5, /**< Model E, static type (5). */
        StaticSimulator = 7, /**< Rower simulator type (7). */
        StaticDynamic = 8, /**< Dynamic, static type (8). */
        SlidesA = 16, /**< Model A, slides type (16). */
        SlidesB = 17, /**< Model B, slides type (17). */
        SlidesC = 18, /**< Model C, slides type (18). */
        SlidesD = 19, /**< Model D, slides type (19). */
        SlidesE = 20, /**< Model E, slides type (20). */
        LinkedDynamic = 32, /**< Dynamic, linked type (32). */
        StaticDyno = 64, /**< Dynomometer, static type (32). */
        StaticSki = 128, /**< Ski Erg, static type (128). */
        StaticSkiSimulator = 143, /**< Ski simulator type (143). */
        Bike = 192, /**< Bike, no arms type (192). */
        BikeArms = 193, /**< Bike, arms type (193). */
        BikeNoArms = 194, /**< Bike, no arms type (194). */
        BikeSimulator = 207, /**< Bike simulator type (207). */
        MultiErgRow = 224, /**< Multi-erg row type (224). */
        MultiErgSki = 225, /**< Multi-erg ski type (225). */
        MultiErgBike = 226, /**< Multi-erg bike type (226). */
        ErgMachineTypes = 227/**< Number of machine types (227). */
    }
}
